# Axcel

A modular set of movement scripts to allow easy prototyping and further development for 2D platformers made on the Unity platform.

Originally created in Unity version 2021.1.17f1, and compatible with all versions using the same input system.

### **Usage**

Slap the AccelEngine on a gameobject, then either add a custom input manager, or the one provided in the example.

The engine script will automatically add a rigidbody2D component and a capsule collider if it cannot find one, so make sure this is placed on the root of your character.

Go in the ***modules*** folder, and drag and drop any modules you need, play with the sliders until you find the settings that you need.
Included in the base package are:
- walking
- advanced jumping
- shoving

#### **Development and future**

There is no limit to how many modules you can add, and there is a blank template to create your own additional modules. (This will be used later in development for updates, so stay tuned)
For easier development of other modules, the engine stores 8 points that are assigned dynamically depending on the base collider, which allow for defining easier raycasts. These arenumbered 0-7, 0 being positioned above the character, and the others going clockwise in a 45°, and being slightly offset from corners to avoid weird collisions.




### **Copyrighting**

Copyright 2022 Lyall Kindmurr

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files Accel Engine, to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Engine is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.