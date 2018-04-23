using System;
using System.Collections.Generic;
using System.Linq;
using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static System.Math;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;

namespace Fusee.Tutorial.Core
{
    public class Drive : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneContainer _scene1;
        private SceneRenderer _sceneRenderer;
        private SceneRenderer _sceneRenderer1;
        private float _camAngle = 1;
        private TransformComponent _baseTransform;
        private TransformComponent _unterarmTransform;
        private TransformComponent _verbagTransform;
        private TransformComponent _oberesGewTransform;
        private TransformComponent _RadR01Transform;
        private TransformComponent _RadR02Transform;
        private TransformComponent _RadR03Transform;
        private TransformComponent _RadR04Transform;
        private TransformComponent _RadR05Transform;
        private TransformComponent _RadR06Transform;
        private TransformComponent _planeTransform;
        //private TransformComponent _Pallete;
        //private TransformComponent _oberarmTransform;
        private TransformComponent _carTransform;
        private TransformComponent _trailerTransform;
        private ScenePicker _scenePicker;
        private PickResult _currentPick;
        private float3 _oldColor;
        private float _Pallete;
        private float3 pAlt;
        private float3 pAneu;
        private float _d = 15;
        private float3 pBalt;

      

        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intentsity in all color channels R, G, B, A).
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);


            _scene = AssetStorage.Get<SceneContainer>("Fahrzeug8.fus");
            _oberesGewTransform = _scene.Children.FindNodes(node => node.Name == "pfeiler_gewinde_arm_nicht_drehbar")?.FirstOrDefault()?.GetTransform();
            _RadR01Transform = _scene.Children.FindNodes(node => node.Name == "Rad_R_01")?.FirstOrDefault()?.GetTransform();
            _RadR02Transform = _scene.Children.FindNodes(node => node.Name == "Rad_L_01")?.FirstOrDefault()?.GetTransform();
            _RadR03Transform = _scene.Children.FindNodes(node => node.Name == "Rad_02_R")?.FirstOrDefault()?.GetTransform();
            _RadR04Transform = _scene.Children.FindNodes(node => node.Name == "Rad_02_L")?.FirstOrDefault()?.GetTransform();
            _RadR05Transform = _scene.Children.FindNodes(node => node.Name == "Rad_03_L")?.FirstOrDefault()?.GetTransform();
            _RadR06Transform = _scene.Children.FindNodes(node => node.Name == "Rad_03_R")?.FirstOrDefault()?.GetTransform();
            _unterarmTransform = _scene.Children.FindNodes(node => node.Name == "unterer_arm")?.FirstOrDefault()?.GetTransform(); //Oberer_arm
            //_oberarmTransform = _scene.Children.FindNodes(node => node.Name == "Oberer_arm")?.FirstOrDefault()?.GetTransform();
            _verbagTransform = _scene.Children.FindNodes(node => node.Name == "verbindung_arm_greifer")?.FirstOrDefault()?.GetTransform();
            _carTransform = _scene.Children.FindNodes(node => node.Name == "cassis")?.FirstOrDefault()?.GetTransform();
            //_Pallete = _scene.Children.FindNodes(node => node.Name == "Pallete")?.FirstOrDefault()?.GetTransform();

            _Pallete = _verbagTransform.Translation.y;



            _trailerTransform = new TransformComponent { Rotation = new float3(-M.Pi / 5.7f, 0, 0), Scale = float3.One, Translation = new float3(0, 0, -10) };

            _scene.Children.Add(new SceneNodeContainer
            {
                Components = new List<SceneComponentContainer>
                {
                    _trailerTransform,
                    new MaterialComponent { Diffuse = new MatChannelContainer { Color = new float3(0.7f, 0.7f, 0.7f) }, Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }},
                    SimpleMeshes.CreateCuboid(new float3(2, 2, 2))
                }
            });
            _baseTransform = new TransformComponent { Rotation = new float3(0, 0, 0), Scale = float3.One, Translation = new float3(100, 100, 0) };

            _scene.Children.Add(new SceneNodeContainer
            {
                Components = new List<SceneComponentContainer>
                {
                    _baseTransform,
                    new MaterialComponent { Diffuse = new MatChannelContainer { Color = new float3(1f, 0.7f, 0.7f) }, Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }},
                    SimpleMeshes.CreateCuboid(new float3(2, 2, 2))
                }
            });

            //Scene Picker
            _scenePicker = new ScenePicker(_scene);

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
            //_sceneRenderer1 = new SceneRenderer(_scene1);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            //_trailerTransform.Rotation = new float3(0,0,0);

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            if (Mouse.RightButton == true)
            {
                _camAngle += Mouse.Velocity.x * 0.00001f * DeltaTime / 20 * 10000;
            }

         


            // Setup the camera 
            //RC.View = float4x4.CreateTranslation(0,-5,40) * float4x4.CreateRotationY(pBalt.y);
           
            //RC.View = float4x4.CreateTranslation(pBneu) * float4x4.CreateRotationY(_camAngle);
            RC.View = float4x4.CreateRotationX(-M.Pi / 7.3f) * float4x4.CreateRotationY(M.Pi - _trailerTransform.Rotation.y) * float4x4.CreateTranslation(-_trailerTransform.Translation.x, -6, -_trailerTransform.Translation.z);
            //ToDO If Bedingung eingefügt

            if (Mouse.LeftButton)
            {
                float2 pickPosClip = Mouse.Position * new float2(2.0f / Width, -2.0f / Height) + new float2(-1, 1);
                _scenePicker.View = RC.View;
                _scenePicker.Projection = RC.Projection;
                List<PickResult> pickResults = _scenePicker.Pick(pickPosClip).ToList();
                if (pickResults.Count > 0)
                {
                    Diagnostics.Log(pickResults[0].Node.Name);

                    PickResult newPick = null;
                    if (pickResults.Count > 0)
                    {
                        pickResults.Sort((a, b) => Sign(a.ClipPos.z - b.ClipPos.z));
                        newPick = pickResults[0];
                    }
                    if (newPick?.Node != _currentPick?.Node)
                    {
                        if (_currentPick != null)
                        {
                            _currentPick.Node.GetMaterial().Diffuse.Color = _oldColor;
                        }
                        if (newPick != null)
                        {
                            var mat = newPick.Node.GetMaterial();
                            _oldColor = mat.Diffuse.Color;
                            mat.Diffuse.Color = new float3(1, 0.4f, 0.4f);

                            _Pallete = newPick.Node.GetTransform().Translation.y - 2.5f;

                        }
                        _currentPick = newPick;
                    }
                }
            }


            if (_currentPick != null)
            {
                
                if (_currentPick.Node.Name == "Rad_R_01")
                {
                    float RadR01 = _RadR01Transform.Rotation.x;
                    RadR01 += Keyboard.ADAxis * 2.0f * (DeltaTime);
                    _RadR01Transform.Rotation = new float3(RadR01, 0, 0);
                }
                else if (_currentPick.Node.Name == "Rad_L_01")
                {
                    float RadR02 = _RadR02Transform.Rotation.x;
                    RadR02 += Keyboard.ADAxis * 2.0f * (DeltaTime);
                    _RadR02Transform.Rotation = new float3(RadR02, 0, 0);
                }
                else if (_currentPick.Node.Name == "Rad_02_R")
                {
                    float RadR03 = _RadR03Transform.Rotation.x;
                    RadR03 += Keyboard.ADAxis * 2.0f * (DeltaTime);
                    _RadR03Transform.Rotation = new float3(RadR03, 0, 0);
                }
                else if (_currentPick.Node.Name == "Rad_02_L")
                {
                    float RadR04 = _RadR04Transform.Rotation.x;
                    RadR04 += Keyboard.ADAxis * 2.0f * (DeltaTime);
                    _RadR04Transform.Rotation = new float3(RadR04, 0, 0);
                }
                else if (_currentPick.Node.Name == "Rad_03_L")
                {
                    float RadR05 = _RadR05Transform.Rotation.x;
                    RadR05 += Keyboard.ADAxis * 2.0f * (DeltaTime);
                    _RadR05Transform.Rotation = new float3(RadR05, 0, 0);
                }
                else if (_currentPick.Node.Name == "Rad_03_R")
                {
                    float RadR06 = _RadR06Transform.Rotation.x;
                    RadR06 += Keyboard.ADAxis * 2.0f * (DeltaTime);
                    _RadR06Transform.Rotation = new float3(RadR06, 0, 0);
                }
                else if (_currentPick.Node.Name == "Pallete")
                {

                }



                if (_currentPick.Node.Name == "pfeiler_gewinde_arm_nicht_drehbar")
                {
                    float ogd = _oberesGewTransform.Rotation.y;
                    ogd += Keyboard.LeftRightAxis * 0.005f;
                    _oberesGewTransform.Rotation = new float3(0, ogd, 0);
                }
                else if (_currentPick.Node.Name == "unterer_arm")
                {
                    float unter = _unterarmTransform.Rotation.x;
                    if (Keyboard.GetKey(KeyCodes.Up) == true)
                    {
                        if (unter <= 0.35f)
                        {

                            unter += DeltaTime * 0.1f;
                            _unterarmTransform.Rotation = new float3(unter, 0, 0);
                        }
                    }
                    if (Keyboard.GetKey(KeyCodes.Down) == true)
                    {
                        if (unter >= 0.0f)
                        {

                            unter -= DeltaTime * 0.1f;
                            _unterarmTransform.Rotation = new float3(unter, 0, 0);
                        }
                    }
                }
                /*case "Oberer_arm":
                    float obArmx = _oberarmTransform.Translation.z;
                    obArmx += Keyboard.UpDownAxis * 0.1f;
                    /*float obArmy = _oberarmTransform.Translation.z;
                    obArmy += Keyboard.UpDownAxis * 0.1f;*/
                /*_oberarmTransform.Translation = new float3(0, 0, obArmx);
                break;*/
                else if (_currentPick.Node.Name == "verbindung_arm_greifer")
                {
                    float ver = _verbagTransform.Translation.y;
                    if (Keyboard.GetKey(KeyCodes.Up) == true)
                    {
                        if (ver <= -0.5f)
                        {
                            ver += DeltaTime * 2f;
                            _verbagTransform.Translation = new float3(0, ver, 0);
                        }
                    }
                    if (Keyboard.GetKey(KeyCodes.Down) == true)
                    {
                        if (ver >= -10.0f)
                        {

                            ver -= DeltaTime * 2f;
                            _verbagTransform.Translation = new float3(0, ver, 0);
                        }
                    }
                }
            }
            float Rad_03 = _RadR03Transform.Rotation.x;
            Rad_03 += Keyboard.WSAxis * 0.15f * (DeltaTime / 16 * 1000);
            _RadR03Transform.Rotation = new float3(Rad_03, 0, 0);

            float Rad_04 = _RadR04Transform.Rotation.x;
            Rad_04 += Keyboard.WSAxis * 0.15f * (DeltaTime / 16 * 1000);
            _RadR04Transform.Rotation = new float3(Rad_04, 0, 0);

            float Rad_05 = _RadR05Transform.Rotation.x;
            Rad_05 += Keyboard.WSAxis * 0.15f * (DeltaTime / 16 * 1000);
            _RadR05Transform.Rotation = new float3(Rad_05, 0, 0);

            float Rad_06 = _RadR06Transform.Rotation.x;
            Rad_06 += Keyboard.WSAxis * 0.15f * (DeltaTime / 16 * 1000);
            _RadR06Transform.Rotation = new float3(Rad_06, 0, 0);

            float Rad_01x = _RadR01Transform.Rotation.x;
            float Rad_01y = _RadR01Transform.Rotation.y;
            Rad_01x += Keyboard.WSAxis * 0.15f * (DeltaTime / 16 * 1000);
            Rad_01y = -Keyboard.ADAxis * -0.35f;
            _RadR01Transform.Rotation = new float3(Rad_01x, Rad_01y, 0);

            float Rad_02x = _RadR02Transform.Rotation.x;
            float Rad_02y = _RadR02Transform.Rotation.y;
            Rad_02x += Keyboard.WSAxis * 0.15f * (DeltaTime / 16 * 1000);
            Rad_02y = -Keyboard.ADAxis * -.35f;
            _RadR02Transform.Rotation = new float3(Rad_02x, Rad_02y, 0);

             float xArm = _unterarmTransform.Rotation.x;
            float yVerbag = _verbagTransform.Translation.y;
            if ( _Pallete < yVerbag)
            {
                yVerbag -= DeltaTime;
                if(_Pallete > yVerbag)
                {
                    yVerbag = _Pallete;
                }
            }

            if (_Pallete > yVerbag)
            {
                //xArm -= DeltaTime;
                yVerbag += DeltaTime;
                if (_Pallete < yVerbag)
                {
                    //xArm = _Pallete;
                    yVerbag = _Pallete;
                }
            }
            
            
            
            _verbagTransform.Translation = new float3(_verbagTransform.Translation.x, yVerbag, _verbagTransform.Translation.z);
            

            float3 pAalt = _carTransform.Translation;
            float3 pBalt = _trailerTransform.Translation;

            float posVel = - Keyboard.WSAxis * Time.DeltaTime;
            float rotVel = Keyboard.ADAxis * Time.DeltaTime;

            float newRot = _carTransform.Rotation.y + (rotVel * Keyboard.WSAxis * Time.DeltaTime * 30);
            _carTransform.Rotation = new float3(0, newRot, 0);

            float3 pAneu = _carTransform.Translation + new float3(posVel * M.Sin(newRot) * 10, 0, posVel * M.Cos(newRot) * 10);
            _carTransform.Translation = pAneu;

            float3 pBneu = pAneu + (float3.Normalize(pBalt - pAneu) * _d);
            _trailerTransform.Translation = pBneu;

            _trailerTransform.Rotation = new float3(0, (float)System.Math.Atan2(float3.Normalize(pBalt - pAneu).x, float3.Normalize(pBalt - pAneu).z), 0);

            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered farame) on the front buffer.
            Present();
        }


        // Is called when the window was resized
        public override void Resize()
        {
            // Set the new rendering area to the entire new windows size
            RC.Viewport(0, 0, Width, Height);

            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;

            // 0.25*PI Rad -> 45° Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }
    }
}
