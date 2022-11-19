using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Scripts.Gameplay.GameField
{
    public class FieldBorder
    {
        private Vector2 fieldBound;
        private List<IFieldObject> fieldObjects;

        public FieldBorder(Vector2 fieldBound)
        {
            this.fieldBound = fieldBound;
            fieldObjects = new List<IFieldObject>();
        }

        public void Add(IFieldObject fieldObject)
        {
            fieldObjects.Add(fieldObject);
        }

        public void Remove(IFieldObject fieldObject)
        {
            fieldObjects.Remove(fieldObject);
        }

        public void CheckBorderIntersection()
        {
            foreach (var fieldObject in fieldObjects)
            {
                if (fieldObject.Position.x > fieldBound.x)
                    fieldObject.Position = new Vector2(-fieldBound.x, fieldObject.Position.y);
                if (fieldObject.Position.x < -fieldBound.x)
                    fieldObject.Position = new Vector2(fieldBound.x, fieldObject.Position.y);
                if (fieldObject.Position.y > fieldBound.y)
                    fieldObject.Position = new Vector2(fieldObject.Position.x, -fieldBound.y);
                if (fieldObject.Position.y < -fieldBound.y)
                    fieldObject.Position = new Vector2(fieldObject.Position.x, fieldBound.y);
            }
        }
    }
}