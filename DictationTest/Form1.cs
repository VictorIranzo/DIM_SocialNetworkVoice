namespace DictationTest
{
    using System;
    using System.Speech.Recognition;
    using System.Speech.Synthesis;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private SpeechRecognitionEngine speechRecognizer = new SpeechRecognitionEngine();
        private SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Grammar grammar = new DictationGrammar();
            speechRecognizer.SetInputToDefaultAudioDevice();
            speechRecognizer.UnloadAllGrammars();
            speechRecognizer.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 60);
            grammar.Enabled = true;
            speechRecognizer.LoadGrammar(grammar);
            speechRecognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

            speechRecognizer.InitialSilenceTimeout = TimeSpan.FromSeconds(3);
            speechRecognizer.BabbleTimeout = TimeSpan.FromSeconds(1);
            speechRecognizer.EndSilenceTimeout = TimeSpan.FromSeconds(1);
            speechRecognizer.EndSilenceTimeoutAmbiguous = TimeSpan.FromSeconds(0.5);

            speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            SemanticValue semantics = e.Result.Semantics;

            string rawText = e.Result.Text;
            RecognitionResult result = e.Result;

            this.dictationTextBox.Text += rawText;
        }
    }
}
