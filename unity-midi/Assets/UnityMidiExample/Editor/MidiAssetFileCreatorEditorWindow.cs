using UnityEngine;
using UnityEditor;
using System.Collections;
using MidiUtils;

public class MidiAssetFileCreatorEditorWindow : EditorWindow {

	[MenuItem ("Midi/AssetFileCreator")]
	static void Init () {
		MidiAssetFileCreatorEditorWindow window = (MidiAssetFileCreatorEditorWindow) EditorWindow.GetWindow( typeof( MidiAssetFileCreatorEditorWindow ) );
		window.Show();
	}
	
	string midiInputPath;

	bool useParsingLog = false;
	string parsingLogOutputPath;
	bool useSequencerLog = false;
	string sequencerLogOutputPath;
	
	string assetOutputPath;

	void OnGUI() {

		EditorGUILayout.Space();
		InputPathControl( "Song input path:", "mid", ref midiInputPath );

		useParsingLog = EditorGUILayout.Foldout( useParsingLog, "Parsing log" );
		if ( useParsingLog ) {
			EditorGUI.indentLevel++;
			OutputPathControl( "Parsing log output path:", "log", ref parsingLogOutputPath );
			EditorGUI.indentLevel--;
		}

		useSequencerLog = EditorGUILayout.Foldout( useSequencerLog, "Sequencer log" );
		if ( useSequencerLog ) {
			EditorGUI.indentLevel++;
			OutputPathControl( "Sequencer log output path:", "log", ref sequencerLogOutputPath );
			EditorGUI.indentLevel--;
		}
		
		EditorGUILayout.Space();
		OutputPathControl( "Asset output path:", "asset", ref assetOutputPath );
		
		if ( GUILayout.Button( "Parse and save .asset!" ) ) {
			ParseSequenceAndSave();
		}

	}

	void InputPathControl( string title, string extension, ref string path ) {
		EditorGUILayout.LabelField( title );
		EditorGUILayout.BeginHorizontal();	
		path = EditorGUILayout.TextField( path );
		if ( GUILayout.Button( "Explore..." ) ) {
			path = AskForInputPath( path, extension );
			EditorGUI.FocusTextInControl( "" );
		}
		EditorGUILayout.EndHorizontal();	
		EditorGUILayout.Space();
	}

	void OutputPathControl( string title, string extension, ref string path ) {
		EditorGUILayout.LabelField( title );
		EditorGUILayout.BeginHorizontal();	
		path = EditorGUILayout.TextField( path );
		if ( GUILayout.Button( "Explore..." ) ) {
			path = AskForOutputPath( path, extension );
			EditorGUI.FocusTextInControl( "" );
		}
		EditorGUILayout.EndHorizontal();	
		EditorGUILayout.Space();
	}
	
	string AskForInputPath( string currentPath, string extension ) {
		string directory = GetDirectoryFrom( currentPath );
		string result = EditorUtility.OpenFilePanel( "Output path", directory, extension );
		EditorGUI.FocusTextInControl( "" );
		return result;
	}
	
	string AskForOutputPath( string currentPath, string extension ) {
		string directory = GetDirectoryFrom( currentPath );
		string result = EditorUtility.SaveFilePanel( "Output path", directory, "output", extension );
		EditorGUI.FocusTextInControl( "" );
		return result;
	}

	string GetDirectoryFrom( string path ) {
		if ( string.IsNullOrEmpty( path ) ) {
			path = Application.dataPath + "/Resources/";
		}
		int lastIndexOfSlash = path.LastIndexOf( '/' );
		return path.Substring( 0, ( lastIndexOfSlash >= 0 ? lastIndexOfSlash : path.Length ) );
	}

	void ParseSequenceAndSave() {

		string inputPath = midiInputPath;
		AssertPathOK( inputPath );

		string outputPath = assetOutputPath;
		AssertPathOK( outputPath );

		MidiLoader.Loader loader = new MidiLoader.Loader( inputPath );
		if ( useParsingLog ) {
			AssertPathOK( parsingLogOutputPath );
			MidiLoader.LoaderLogger.LogTo( loader, parsingLogOutputPath );
		}

		MidiFakeSequencer.Sequencer sequencer = new MidiFakeSequencer.Sequencer( loader );
		if ( useSequencerLog ) {
			AssertPathOK( sequencerLogOutputPath );
			MidiFakeSequencer.SequencerLogger.LogTo( sequencer, sequencerLogOutputPath );
		}

		/* It's required for AssetDatabase to have paths relative to project directory */
		outputPath = outputPath.Substring( outputPath.IndexOf( "Assets" ) );
		CreateAndSaveAsset( sequencer.fakeTracks, outputPath );
	}
	
	void AssertPathOK( string path ) {
		if ( string.IsNullOrEmpty( path ) ) {
			throw new System.Exception( "Invalid path: " + path );
		}
	}

	void CreateAndSaveAsset( MidiFakeSequencer.Sequencer.FakeTrack[] tracks, string relativeOutputPath ) {
		MidiUtils.TimeSpanLogger.Begin();
		try {

			MidiFakeSequencer.SequenceAsset asset =
				AssetDatabase.LoadAssetAtPath( relativeOutputPath, typeof( MidiFakeSequencer.SequenceAsset ) )
					as MidiFakeSequencer.SequenceAsset;
			if ( asset == null ) {
				asset = ScriptableObject.CreateInstance<MidiFakeSequencer.SequenceAsset>();
				AssetDatabase.CreateAsset( asset, relativeOutputPath );
			}

			asset.tracks = tracks;
			EditorUtility.SetDirty( asset );

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

		} catch ( System.Exception e ) {
			Debug.Log( "Couldn't do with relative path: " + relativeOutputPath );
			Debug.LogError( e.ToString() );
		}
		MidiUtils.TimeSpanLogger.EndAndLog( "Saved asset file" );
	}
	
}