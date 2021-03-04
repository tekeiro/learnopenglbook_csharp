﻿namespace LearnOpenGLBook_CSharp.Library.Types
{
    /// <summary>
    /// Represent a RGB Color
    /// </summary>
    public struct Color
    {
        public float red;
        public float green;
        public float blue;
        public float alpha;

        public Color(float red, float green, float blue, float alpha)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;
        }
        
    }
}