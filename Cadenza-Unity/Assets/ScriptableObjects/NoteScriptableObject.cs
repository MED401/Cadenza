using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Create New Note")]
    public class NoteScriptableObject : ScriptableObject
    {
        public AudioClip clip;
    }
}
