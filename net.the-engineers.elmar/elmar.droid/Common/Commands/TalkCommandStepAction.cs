namespace elmar.droid.Common.Commands
{
    class TalkCommandStepAction : ICommandStepAction
    {
        private readonly VoiceOutput _voiceOutput;

        public TalkCommandStepAction(VoiceOutput voiceOutput)
        {
            _voiceOutput = voiceOutput;
        }

        public void Execute(string parameter)
        {
            _voiceOutput.Speek(parameter);
        }
    }

}