using Interactions;
using ScriptableObjects;

namespace LevelComponents.SolutionElements.Buttons
{
    public class PitchSelector : Interactable
    {
        public int index;
        public NoteScriptableObject note;
        private SoundObjectFactory _soundObjectFactory;

        protected override void Start()
        {
            UseInfo = "Change Pitch";
            _soundObjectFactory = transform.parent.parent.GetComponent<SoundObjectFactory>();
        }

        public override void Interact()
        {
            _soundObjectFactory.SetPitch(note, index);
        }
    }
}