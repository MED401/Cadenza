using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Create New Instrument")]
    public class InstrumentScriptableObject : ScriptableObject
    {
        public NoteScriptableObject[] notes;
    }
}