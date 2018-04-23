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
    public class FirstSteps : RenderCanvas
    {
        //Scene und Renderer eingefügt. Aufgaben Teil 01
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;

        private SceneContainer _scene1;
        private SceneRenderer _sceneRenderer1;

        private SceneContainer _scene2;
        private SceneRenderer _sceneRenderer2;


        // Teil 02
        private float _camAngle = 0;

        //Teil 03
        private TransformComponent _cubeTransform;

        private TransformComponent _cubeTransform1;
        private TransformComponent _cubeTransform2;


        // Scale Variable
        // private float y = 0;


        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intentsity in all color channels R, G, B, A).
            RC.ClearColor = new float4(1, 1, 1, 1);

            // Neuen Würfel Erstellen 
            /*var cubeTransform = new TransformComponent { Scale = new float3(1, 1, 1),/*Würfel zum Rechteck machen */
            /* Translation = new float3(0, 0, 0)}; // Drehung und Posiotion des Würfels*/

            _cubeTransform = new TransformComponent { Scale = new float3(1, 1, 1), Translation = new float3(0, 0, 0) };
            var cubeMaterial = new MaterialComponent
            {
                Diffuse = new MatChannelContainer { Color = new float3(0.5f, 0.7f, 0) }, //Farbe ändern. aber wie richtig ?!
                Specular = new SpecularChannelContainer { Color = float3.One, Shininess = 4 }
            };
            var cubeMesh = SimpleMeshes.CreateCuboid(new float3(10, 10, 10)); // Skalierung des Würfels // Würfel zum Rechteck machen

            var cubeNode = new SceneNodeContainer();
            cubeNode.Components = new List<SceneComponentContainer>();
            /*cubeNode.Components.Add(cubeTransform);*/
            cubeNode.Components.Add(_cubeTransform);
            cubeNode.Components.Add(cubeMaterial);
            cubeNode.Components.Add(cubeMesh);

            _scene = new SceneContainer();
            _scene.Children = new List<SceneNodeContainer>();
            _scene.Children.Add(cubeNode);

            _sceneRenderer = new SceneRenderer(_scene);

            
            // Würfel 02
            _cubeTransform1 = new TransformComponent { Scale = new float3(1, 1, 1), Translation = new float3(0, 0, 0) };
            var cubeMaterial1 = new MaterialComponent
            {
                Diffuse = new MatChannelContainer { Color = new float3(0.5f, 1.0f, 0.5f) }, //Farbe ändern. aber wie richtig ?!
                Specular = new SpecularChannelContainer { Color = float3.One, Shininess = 4 }
            };
            var cubeMesh1 = SimpleMeshes.CreateCuboid(new float3(1, 1, 1)); // Skalierung des Würfels // Würfel zum Rechteck machen

            var cubeNode1 = new SceneNodeContainer();
            cubeNode1.Components = new List<SceneComponentContainer>();
            /*cubeNode.Components.Add(cubeTransform);*/
            
            cubeNode1.Components.Add(_cubeTransform1);
            cubeNode1.Components.Add(cubeMaterial1);
            cubeNode1.Components.Add(cubeMesh1);

            _scene1 = new SceneContainer();
            _scene1.Children = new List<SceneNodeContainer>();
            _scene1.Children.Add(cubeNode1);

            _sceneRenderer1 = new SceneRenderer(_scene1);

            // Würfel 03
            _cubeTransform2 = new TransformComponent { Scale = new float3(1, 1, 1), Translation = new float3(0, 0, 0) };
            var cubeMaterial2 = new MaterialComponent
            {
                Diffuse = new MatChannelContainer { Color = new float3(0, 2 * M.Sin(0.2f * TimeSinceStart), 0.5f) }, //Farbe ändern. aber wie richtig ?!
                Specular = new SpecularChannelContainer { Color = float3.One, Shininess = 4 }
            };
            var cubeMesh2 = SimpleMeshes.CreateCuboid(new float3(1, 1, 1)); // Skalierung des Würfels // Würfel zum Rechteck machen

            var cubeNode2 = new SceneNodeContainer();
            cubeNode2.Components = new List<SceneComponentContainer>();
            /*cubeNode.Components.Add(cubeTransform);*/

            cubeNode2.Components.Add(_cubeTransform2);
            cubeNode2.Components.Add(cubeMaterial2);
            cubeNode2.Components.Add(cubeMesh2);

            _scene2 = new SceneContainer();
            _scene2.Children = new List<SceneNodeContainer>();
            _scene2.Children.Add(cubeNode2);

            _sceneRenderer2 = new SceneRenderer(_scene2);
        }
        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            //Methode eingefügt
            _sceneRenderer.Render(RC); // Blauer Würfel wird nun angezeigt.

            //Animate the cube 
            _cubeTransform.Translation = new float3(2 * M.Sin(7 * TimeSinceStart), 10* M.Sin(0.5f * TimeSinceStart), 0);


            //Zweiter Würfel Scene eingefügt 
            _sceneRenderer1.Render(RC);
            //Animate the cube 
            _cubeTransform1.Translation = new float3(-25, 0, 5 * M.Sin(0.2f * TimeSinceStart));

            // Change Scale over Time
            _cubeTransform1.Scale = new float3( 1, 1, 5 * M.Sin(0.2f * TimeSinceStart));


            /*  for (int i = 0; i < 10; i++) {
                      y++;
                      _cubeTransform1.Scale = new float3(1, 1, y);
                  if (y == 10)
                  {
                      for(int j = 0; j < 10; j++)
                      {
                          y--;
                          _cubeTransform1.Scale = new float3(1, 1, y);
                      }
                  }
              }*/

            //30. Würfel Scene eingefügt 
            _sceneRenderer2.Render(RC);

            _cubeTransform2.Translation = new float3(10, 5 * M.Sin(5 * TimeSinceStart), 0);
            _cubeTransform2.Scale = new float3(1, 1 * M.Sin(5.0f * TimeSinceStart), 5);





            //Camera Winkel
            _camAngle += 90.0f * M.Pi/180.0f * DeltaTime;

            //Camera Setup
            RC.View = float4x4.CreateTranslation(0, 0, 50) * float4x4.CreateRotationY(_camAngle);


            // Swap buffers: Show the contents of the backbuffer (containing the currently rerndered farame) on the front buffer.
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

        private class _cubeDiffuse
        {
        }
    }
}