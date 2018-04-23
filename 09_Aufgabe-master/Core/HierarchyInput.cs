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
    public class HierarchyInput : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        private float _camAngle = 0;
        private TransformComponent _baseTransform;

        //body Transform
        private TransformComponent _bodyTransform;

        //Upper Arm Transform
        private TransformComponent _upperArmTransform;

        //Unter  Arm Transform
        private TransformComponent _unterArmTransform;

        //Greifer 01
        private TransformComponent _greifer01;

        //Greifer 02
        private TransformComponent _greifer02;
        //Greifer 03
        private TransformComponent _greifer03;


        // Kamera Angle
        private TransformComponent _cameraAngle;

        // Automatisches Öffnen und Schließen
        private Boolean open = false;
        private Boolean close = false;


        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };

            // Initialisierung des Body Element 
            _bodyTransform = new TransformComponent
            {
                Rotation = new float3(0, 2.2f, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 6, 0)
            };

            // Initialisierung des Grünen Armes
           _upperArmTransform = new TransformComponent
            {
               Rotation = new float3(1.5f, 0, 0),
               Scale = new float3(1, 1, 1),
               Translation = new float3(2, 4, 0)
           };
            // Initialisierung des Blauen Armes
            _unterArmTransform = new TransformComponent
            {
                Rotation = new float3(0.4f, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(-2, 8, 0)
            };

            // Initalisierung des 1 Greifers
            _greifer01 = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(-1, 6, 0)
            };

            // Initalisierung des 1 Greifers
            _greifer02 = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(1, 6, 0)
            };

            // Setup the scene graph
            return new SceneContainer
            {
                Children = new List<SceneNodeContainer>
                {
                    // Graue Base 
                    new SceneNodeContainer
                    {
                        Components = new List<SceneComponentContainer>
                        {
                            // TRANSFROM COMPONENT
                            _baseTransform,

                            // MATERIAL COMPONENT
                            new MaterialComponent
                            {
                                Diffuse = new MatChannelContainer { Color = new float3(0.7f, 0.7f, 0.7f) },
                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                            },

                            // MESH COMPONENT
                            SimpleMeshes.CreateCuboid(new float3(10, 2, 10))
                        }
                    },
                    // Red Body 
                    new SceneNodeContainer
                    {
                        Components = new List<SceneComponentContainer>
                        {
                            _bodyTransform,
                            new MaterialComponent
                            {
                                Diffuse = new MatChannelContainer { Color = new float3(1, 0, 0) },
                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                            },
                            SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                        },
                        Children = new List<SceneNodeContainer>
                        {
                            // Grüner Upper Arm
                            new SceneNodeContainer
                            {
                                Components = new List<SceneComponentContainer>
                                {
                                    _upperArmTransform,
                                },
                                Children = new List<SceneNodeContainer>
                                {
                                    new SceneNodeContainer
                                    {
                                        Components = new List<SceneComponentContainer>
                                        {
                                            new TransformComponent
                                            {
                                                Rotation = new float3(0, 0, 0),
                                                Scale = new float3(1, 1, 1),
                                                Translation = new float3(0, 4, 0)
                                            },
                                            new MaterialComponent
                                            {
                                                Diffuse = new MatChannelContainer { Color = new float3(0, 1, 0) },
                                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                            },
                                            SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                        }
                                    },
                                    // Blauer Unterarm
                                    new SceneNodeContainer
                                    {
                                        Components = new List<SceneComponentContainer>
                                        {
                                            _unterArmTransform
                                        },
                                        Children = new List<SceneNodeContainer>
                                        {
                                            new SceneNodeContainer
                                            {
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    new TransformComponent
                                                    {
                                                        Rotation = new float3(0, 0, 0),
                                                        Scale = new float3(1, 1, 1),
                                                        Translation = new float3(0, 4, 0)
                                                    },
                                                    new MaterialComponent
                                                    {
                                                        Diffuse = new MatChannelContainer { Color = new float3(0, 0, 1) },
                                                        Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                                    },
                                                    SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                                }
                                            },
                                            //Greifer01
                                    new SceneNodeContainer
                                    {
                                        Components = new List<SceneComponentContainer>
                                        {
                                            _greifer01
                                        },
                                        Children = new List<SceneNodeContainer>
                                        {
                                            new SceneNodeContainer
                                            {
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    new TransformComponent
                                                    {
                                                        Rotation = new float3(0, 0, 0),
                                                        Scale = new float3(1, 1, 1),
                                                        Translation = new float3(0, 4, 0)
                                                    },
                                                    new MaterialComponent
                                                    {
                                                        Diffuse = new MatChannelContainer { Color = new float3(0, 0, 0) },
                                                        Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                                    },
                                                    SimpleMeshes.CreateCuboid(new float3(1, 2, 1))
                                                }
                                            },

                                        }
                                    },
                                    //Greifer 02
                                    new SceneNodeContainer
                                    {
                                        Components = new List<SceneComponentContainer>
                                        {
                                            _greifer02
                                        },
                                        Children = new List<SceneNodeContainer>
                                        {
                                            new SceneNodeContainer
                                            {
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    new TransformComponent
                                                    {
                                                        Rotation = new float3(0, 0, 0),
                                                        Scale = new float3(1, 1, 1),
                                                        Translation = new float3(0, 4, 0)
                                                    },
                                                    new MaterialComponent
                                                    {
                                                        Diffuse = new MatChannelContainer { Color = new float3(0, 0, 0) },
                                                        Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                                    },
                                                    SimpleMeshes.CreateCuboid(new float3(1, 2, 1))
                                                }
                                            },

                                        }
                                    },

                                }
                            },
                        }
                    }
                }
          }
        }
    };

 }

        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intentsity in all color channels R, G, B, A).
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);

            _scene = CreateScene();

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {

            //Intellisens
           /* Mouse.Velocity;
            Keyboard.LeftRightAxis;
            Touch.ButtonCount; */

            // Bewegung mit Keyboard Achse

            float bodyRotation = _bodyTransform.Rotation.y;
            bodyRotation += 0.05f * Keyboard.LeftRightAxis;
            _bodyTransform.Rotation = new float3(0, bodyRotation, 0);

            // Bewegung des UnterArm 
            float uar = _unterArmTransform.Rotation.x;
            uar += 0.005f * Keyboard.UpDownAxis;
            _unterArmTransform.Rotation = new float3(uar, 0, 0);

            // Bewegung des Oberarm
            float oberArm = _upperArmTransform.Rotation.x;
            oberArm += 0.005f * Keyboard.ADAxis;
            _upperArmTransform.Rotation = new float3(oberArm, 0, 0);

            //Bewegung der Greifer 
            float greifer01 = _greifer01.Rotation.z;
            float greifer02 = _greifer02.Rotation.z;
            if (Keyboard.GetKey(KeyCodes.W) == true)
            {
                if (greifer01 <= 0.05f)
                {
                    greifer01 += 0.005f * DeltaTime / 20 * 1000;
                    greifer02 -= 0.005f * DeltaTime / 20 * 1000;
                    _greifer01.Rotation = new float3(0, 0, greifer01);
                    _greifer02.Rotation = new float3(0, 0, greifer02);
                }
            }
            if (Keyboard.GetKey(KeyCodes.S) == true)
            {
                if (greifer01 >= -0.10f)
                {
                    greifer01 -= 0.005f * DeltaTime / 20 * 1000;
                    greifer02 += 0.005f * DeltaTime / 20 * 1000;
                    _greifer01.Rotation = new float3(0, 0, greifer01);
                    _greifer02.Rotation = new float3(0, 0, greifer02);
                }
            }

            if (Keyboard.GetKey(KeyCodes.O) == true)
            {
                close = true;
            }
            if ( close == true)
            {
                if (greifer01 <= 0.05f)
                {
                    greifer01 += 0.005f * DeltaTime / 20 * 1000;
                    greifer02 -= 0.005f * DeltaTime / 20 * 1000;
                    _greifer01.Rotation = new float3(0, 0, greifer01);
                    _greifer02.Rotation = new float3(0, 0, greifer02);
                }
                else
                {
                    close = false;
                }
            }

            if (Keyboard.GetKey(KeyCodes.C) == true)
            {
                open = true;
            }
            if (open == true)
            {
                if (greifer01 >= -0.10f)
                {
                    greifer01 -= 0.005f * DeltaTime / 20 * 1000;
                    greifer02 += 0.005f * DeltaTime / 20 * 1000;
                    _greifer01.Rotation = new float3(0, 0, greifer01);
                    _greifer02.Rotation = new float3(0, 0, greifer02);
                }
                else
                {
                    open = false;
                }
            }

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, -10, 50) * float4x4.CreateRotationY(_camAngle);

            // Kamera 

            //float cameraRot = _cameraAngle.Rotation.x;
            if (Mouse.LeftButton == true)
            {
                _camAngle += Mouse.Velocity.x * 0.0001f * DeltaTime / 20 * 1000;
            }

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