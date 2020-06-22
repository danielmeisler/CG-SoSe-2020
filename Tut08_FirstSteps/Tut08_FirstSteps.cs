using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;
using Fusee.Engine.GUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuseeApp
{
    [FuseeApplication(Name = "Tut08_FirstSteps", Description = "Yet another FUSEE App.")]
    public class Tut08_FirstSteps : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        // Init is called on startup. 
        private float _camAngle = 0;
        private Transform _cubeTransform;
        private Transform _cubeTransform2;
        private Transform _cubeTransform3;
        private Transform _cubeTransform4;
        private Transform _cubeTransform5;
        public override void Init()
        {
        // Create a scene with a cube
        // The three components: one XForm, one Material and the Mesh
        _cubeTransform = new Transform {Scale = new float3(1, 1, 1), Translation = new float3(0, 0, 0), Rotation = new float3(0, 0, 0)};
        var cubeShader = ShaderCodeBuilder.MakeShaderEffect(new float4 (50/255f, 100/255f, 180/255f, 1));
        var cubeMesh = SimpleMeshes.CreateCuboid(new float3(10, 10, 10));

        _cubeTransform2 = new Transform{Scale = new float3(2, 2, 2), Translation = new float3(50, 0, 0), Rotation = new float3(10, 10, 10)};
        var cubeShader2 =  ShaderCodeBuilder.MakeShaderEffect(new float4 (1f, 0f, 0f, 1));
        var cubeMesh2 = SimpleMeshes.CreateCuboid(new float3(10, 10, 10));

        _cubeTransform3 = new Transform{Scale = new float3(3, 1, 3), Translation = new float3(0, -20, 0), Rotation = new float3(0, 0, 0)};
        var cubeShader3 =  ShaderCodeBuilder.MakeShaderEffect(new float4 (80/255f, 170/255f, 0/255f, 1));
        var cubeMesh3 = SimpleMeshes.CreateCuboid(new float3(10, 10, 10));

        _cubeTransform4 = new Transform{Scale = new float3(2, 1, 1), Translation = new float3(-50, 0, 0), Rotation = new float3(0, 0, 0)};
        var cubeShader4 =  ShaderCodeBuilder.MakeShaderEffect(new float4 (255/255f, 216/255f, 0/255f, 1));
        var cubeMesh4 = SimpleMeshes.CreateCuboid(new float3(10, 10, 10));
        
        _cubeTransform5 = new Transform{Scale = new float3(1, 1, 1), Translation = new float3(0, 18, 0), Rotation = new float3(0, 0, 0)};
        var cubeShader5 =  ShaderCodeBuilder.MakeShaderEffect(new float4 (128/255f, 128/255f, 128/255f, 1));
        var cubeMesh5 = SimpleMeshes.CreateCuboid(new float3(5, 5, 5));

        // Assemble the cube node containing the three components
        var cubeNode = new SceneNode();
        cubeNode.Components.Add(_cubeTransform);
        cubeNode.Components.Add(cubeShader);
        cubeNode.Components.Add(cubeMesh);

        var cubeNode2 = new SceneNode();
        cubeNode2.Components.Add(_cubeTransform2);
        cubeNode2.Components.Add(cubeShader2);
        cubeNode2.Components.Add(cubeMesh2);

        var cubeNode3 = new SceneNode();
        cubeNode3.Components.Add(_cubeTransform3);
        cubeNode3.Components.Add(cubeShader3);
        cubeNode3.Components.Add(cubeMesh3);

        var cubeNode4 = new SceneNode();
        cubeNode4.Components.Add(_cubeTransform4);
        cubeNode4.Components.Add(cubeShader4);
        cubeNode4.Components.Add(cubeMesh4);

        var cubeNode5 = new SceneNode();
        cubeNode5.Components.Add(_cubeTransform5);
        cubeNode5.Components.Add(cubeShader5);
        cubeNode5.Components.Add(cubeMesh5);
        // Create the scene containing the cube as the only object
        _scene = new SceneContainer();
        _scene.Children.Add(cubeNode);
        _scene.Children.Add(cubeNode2);
        _scene.Children.Add(cubeNode3);
        _scene.Children.Add(cubeNode4);
        _scene.Children.Add(cubeNode5);

        // Create a scene renderer holding the scene above
        _sceneRenderer = new SceneRendererForward(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
             SetProjectionAndViewport();

             RC.ClearColor = new float4(250/255 * M.Sin(3 * TimeSinceStart) + 1, 1 * M.Sin(3 * TimeSinceStart) + 1, 10/255 * M.Sin(3 * TimeSinceStart) + 1, 0);

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            _camAngle = _camAngle + 90.0f * M.Pi/180.0f * DeltaTime;
            _cubeTransform.Translation = new float3(0, 5 * M.Sin(3 * TimeSinceStart), 0);
            _cubeTransform2.Rotation = new float3( 5 * TimeSinceStart, 0, 0);
            _cubeTransform3.Rotation = new float3(0, 5 * M.Sin(3 * TimeSinceStart), 0);
            _cubeTransform4.Translation = new float3(50 * M.Sin(3 * TimeSinceStart) - 70, 0, 0);
            _cubeTransform5.Rotation = new float3(0, TimeSinceStart, 0);
            _cubeTransform5.Scale = new float3( 5 * M.Sin(3 * TimeSinceStart) + 1 , 1 * M.Sin(3 * TimeSinceStart) + 1, 5 * M.Sin(3 * TimeSinceStart) + 1);
            

             // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, 0, 50) * float4x4.CreateRotationY(_camAngle);

            //cubeShader = new float4(1, 0, 0, 1);

            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }

        public void SetProjectionAndViewport()
        {
            // Set the rendering area to the entire window size
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