using UnityEngine;
 using UnityEditor;
 // ^^^ Make sure you add the UnityEditor using directive! ^^^^
 
 //Also make your script inherit from AssetPostprocessor, NOT Monobehaviour
 public class SetTexImportSizeViaScript : AssetPostprocessor
 {
     //function that's called when a texture starts to be imported
     void OnPreprocessTexture()
     {
         //get a reference to the built-in TextureImporter...
         TextureImporter importer = (TextureImporter)assetImporter;
 
         //create a new empty TextureImporterSettings struct...
         TextureImporterSettings textureImporterSettings = new TextureImporterSettings();
 
         //read the current import settings from the Texture Importer
         //into our new importer settings struct (basically filling the empty struct with values)
         importer.ReadTextureSettings(textureImporterSettings);
 
         //change the maxTextureSize setting in our settings struct
         textureImporterSettings.maxTextureSize = 512;
 
         //pass the settings struct, with the changed maxTextureSize value, back into the importer
         //(e.g. apply the changed settings to the importer)
         importer.SetTextureSettings(textureImporterSettings);
     }
 }