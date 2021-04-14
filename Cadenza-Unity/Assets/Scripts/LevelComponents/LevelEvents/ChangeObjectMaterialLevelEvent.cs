using LevelComponents.SolutionElements;
using ScriptableObjects;
using UnityEngine;

namespace LevelComponents.LevelEvents
{
    public class ChangeObjectMaterialLevelEvent : LevelEvent
    {
        [SerializeField] private GameObject target;
        [SerializeField] private Material material;

        public override void Event(NoteScriptableObject note)
        {
            if (note != correctNoteForEvent) return;

            target.GetComponent<MeshRenderer>().material = material;
        }
    }
}