using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using System.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

/// <summary>
/// Component to help generate a UI Image or RawImage using Stable Diffusion.
/// </summary>
[ExecuteAlways]
public class StableDiffusionReactor : StableDiffusionGenerator
{
    [ReadOnly]
    public string guid = "";

    [SerializeField]
    public string[] samplersList
    {
        get
        {
            if (sdc == null)
                sdc = GameObject.FindObjectOfType<StableDiffusionConfiguration>();
            return sdc.samplers;
        }
    }
    [HideInInspector]
    public int selectedSampler = 0;

    public int width = 512;
    public int height = 512;
    public int steps = 50;
    public float cfgScale = 7;
    public long seed = -1;

    public string sourceFilePath = "Assets/StreamingAssets/Meisje_met_de_parel.png";
    public string targetFilePath = "Assets/StreamingAssets/htm_2016080484837486184.png";

    public long generatedSeed = -1;

    string filename = "";

    [SerializeField]
    public string[] modelsList
    {
        get
        {
            if (sdc == null)
                sdc = GameObject.FindObjectOfType<StableDiffusionConfiguration>();
            return sdc.modelNames;
        }
    }
    [HideInInspector]
    public int selectedModel = 0;
    void Awake()
    {
#if UNITY_EDITOR
        if (width < 0 || height < 0)
        {
            StableDiffusionConfiguration sdc = GameObject.FindObjectOfType<StableDiffusionConfiguration>();
            if (sdc != null)
            {
                SDSettings settings = sdc.settings;
                if (settings != null)
                {

                    width = settings.width;
                    height = settings.height;
                    steps = settings.steps;
                    cfgScale = settings.cfgScale;
                    seed = settings.seed;
                    return;
                }
            }

            width = 512;
            height = 512;
            steps = 50;
            cfgScale = 7;
            seed = -1;
        }
#endif
    }
    void Update()
    {
#if UNITY_EDITOR
        // Clamp image dimensions values between 128 and 2048 pixels
        if (width < 128) width = 128;
        if (height < 128) height = 128;
        if (width > 2048) width = 2048;
        if (height > 2048) height = 2048;

        // If not setup already, generate a GUID (Global Unique Identifier)
        if (guid == "")
            guid = Guid.NewGuid().ToString();
#endif
    }
    bool generating = false;
    public void Generate()
    {

        // Start generation asynchronously
        if (!generating && !string.IsNullOrEmpty(sourceFilePath) && !string.IsNullOrEmpty(targetFilePath))
        {
            StartCoroutine(GenerateAsync());
        }
        else Debug.LogError("Empty Path!", this); return;
    }
    void SetupFolders()
    {
        // Get the configuration settings
        if (sdc == null)
            sdc = GameObject.FindObjectOfType<StableDiffusionConfiguration>();

        try
        {
            // Determine output path
            string root = Application.dataPath + sdc.settings.OutputFolder;
            if (root == "" || !Directory.Exists(root))
                root = Application.streamingAssetsPath;
            string mat = Path.Combine(root, "SDImages");
            filename = Path.Combine(mat, guid + DateTime.Now.ToString("_yyyy-MM-dd_HH-mm-ss") + ".png");

            // If folders not already exists, create them
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);
            if (!Directory.Exists(mat))
                Directory.CreateDirectory(mat);

            // If the file already exists, delete it
            if (File.Exists(filename))
                File.Delete(filename);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message + "\n\n" + e.StackTrace);
        }
    }

    IEnumerator GenerateAsync()
    {
        generating = true;

        SetupFolders();

        // Set the model parameters
        yield return sdc.SetModelAsync(modelsList[selectedModel]);

        // Generate the image
        HttpWebRequest httpWebRequest = null;
        try
        {
            // Make a HTTP POST request to the Stable Diffusion server
            httpWebRequest = (HttpWebRequest)WebRequest.Create(sdc.settings.StableDiffusionServerURL + "/reactor/image");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            // add auth-header to request
            if (sdc.settings.useAuth && !sdc.settings.user.Equals("") && !sdc.settings.pass.Equals(""))
            {
                httpWebRequest.PreAuthenticate = true;
                byte[] bytesToEncode = Encoding.UTF8.GetBytes(sdc.settings.user + ":" + sdc.settings.pass);
                string encodedCredentials = Convert.ToBase64String(bytesToEncode);
                httpWebRequest.Headers.Add("Authorization", "Basic " + encodedCredentials);
            }

            // Send the generation parameters along with the POST request
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {

                SDparamsInReactor sd = new  SDparamsInReactor();
               
                byte[] sourceBytes = File.ReadAllBytes(sourceFilePath);
                byte[] targetBytes = File.ReadAllBytes(targetFilePath);

                string sourceBase64String = "data:image/png;base64," + Convert.ToBase64String(sourceBytes);
                Debug.Log(sourceBase64String);
                string targetBase64String = "data:image/png;base64," + Convert.ToBase64String(targetBytes);
                sd.source_image = sourceBase64String;//sourceImage;
                sd.target_image = targetBase64String;//targetImage;
                //sd.source_faces_index = new int[] { sourceFacesIndex };
                //sd.face_index = new int[] { faceIndex };
                //sd.upscaler = upscaler;
                //sd.scale = scale;
                //sd.upscale_visibility = upscaleVisibility;
                //sd.face_restorer = faceRestorer;
                //sd.restorer_visibility = restorerVisibility;
                //sd.restore_first = restoreFirst;
                //sd.model = model;
                //sd.gender_source = genderSource;
                //sd.gender_target = genderTarget;

                //
                if (selectedSampler >= 0 && selectedSampler < samplersList.Length)
                    sd.sampler_name = samplersList[selectedSampler];

                // Serialize the input parameters
                string json = JsonConvert.SerializeObject(sd);


                // Send to the server
                streamWriter.Write(json);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message + "\n\n" + e.StackTrace);
        }

        // Read the output of generation
        if (httpWebRequest != null)
        {
            // Wait that the generation is complete before procedding
            Task<WebResponse> webResponse = httpWebRequest.GetResponseAsync();

            while (!webResponse.IsCompleted)
            {
                if (sdc.settings.useAuth && !sdc.settings.user.Equals("") && !sdc.settings.pass.Equals(""))
                    UpdateGenerationProgressWithAuth();
                else
                    UpdateGenerationProgress();

                yield return new WaitForSeconds(0.5f);
            }

            // Stream the result from the server
            var httpResponse = webResponse.Result;

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                // Decode the response as a JSON string
                string result = streamReader.ReadToEnd();

                // Deserialize the JSON string into a data structure
                SDResponseImg2Img json = JsonConvert.DeserializeObject<SDResponseImg2Img>(result);
                SDResponseReactor json2 = JsonConvert.DeserializeObject<SDResponseReactor>(result);

                if (string.IsNullOrEmpty(json2.image))
                {
                    Debug.LogError("No image was return by the server. This should not happen. Verify that the server is correctly setup.");

                    generating = false;
#if UNITY_EDITOR
                    EditorUtility.ClearProgressBar();
#endif
                    yield break;
                }
                // Decode the image from Base64 string into an array of bytes
                byte[] imageData = Convert.FromBase64String(json2.image);

                // Write it in the specified project output folder
                using (FileStream imageFile = new FileStream(filename, FileMode.Create))
                {
#if UNITY_EDITOR
                    AssetDatabase.StartAssetEditing();
#endif
                    yield return imageFile.WriteAsync(imageData, 0, imageData.Length);
#if UNITY_EDITOR
                    AssetDatabase.StopAssetEditing();
                    AssetDatabase.SaveAssets();
#endif
                }


                try
                {
                    // Read back the image into a texture
                    if (File.Exists(filename))
                    {
                        Texture2D texture = new Texture2D(2, 2);
                        texture.LoadImage(imageData);
                        texture.Apply();

                        LoadIntoImage(texture);
                    }

                    // As there is no info field in SDReactor, you can remove this part.
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message + "\n\n" + e.StackTrace);
                }
            }
        }
#if UNITY_EDITOR
        EditorUtility.ClearProgressBar();
#endif
        generating = false;
        yield return null;
    }
    void LoadIntoImage(Texture2D texture)
    {
        try
        {
            // Find the image component
            Image im = GetComponent<Image>();
            if (im != null)
            {
                // Create a new Sprite from the loaded image
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                // Set the sprite as the source for the UI Image
                im.sprite = sprite;
            }
            // If no image found, try to find a RawImage component
            else
            {
                RawImage rim = GetComponent<RawImage>();
                if (rim != null)
                {
                    rim.texture = texture;
                }
            }

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                // Force Unity inspector to refresh with new asset
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
                EditorApplication.QueuePlayerLoopUpdate();
                EditorSceneManager.MarkAllScenesDirty();
                EditorUtility.RequestScriptReload();
            }
#endif
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message + "\n\n" + e.StackTrace);
        }
    }
}
