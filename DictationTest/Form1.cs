namespace DictationTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Speech.Recognition;
    using System.Speech.Synthesis;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private SpeechRecognitionEngine speechRecognizer = new SpeechRecognitionEngine();
        private SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
        private Grammar grammar;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            grammar = this.CreateGrammar();
            speechRecognizer.SetInputToDefaultAudioDevice();
            speechRecognizer.UnloadAllGrammars();
            speechRecognizer.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 60);
            grammar.Enabled = true;
            speechRecognizer.LoadGrammar(grammar);
            speechRecognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

            speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        internal Grammar CreateGrammar()
        {
            GrammarBuilder addCommentGrammar = this.CreateAddCommentGrammar();
            GrammarBuilder deleteCommentGrammar = this.CreateDeleteCommentGrammar();

            Choices choices = new Choices();
            choices.Add(addCommentGrammar);
            choices.Add(deleteCommentGrammar);

            Grammar grammar = new Grammar(choices);

            return grammar;
        }

        private GrammarBuilder CreateAddCommentGrammar()
        {
            GrammarBuilder write = "Escribir";
            GrammarBuilder add = "Añadir";
            GrammarBuilder dictate = "Dictar";

            Choices alternativesNavigate = new Choices(write, add, dictate);

            GrammarBuilder sentence = new GrammarBuilder(alternativesNavigate);

            GrammarBuilder comment = "comentario";
            sentence.Append(comment);

            return sentence;
        }

        private GrammarBuilder CreateDeleteCommentGrammar()
        {
            GrammarBuilder deleteComment = "Borrar comentario";

            return deleteComment;
        }

        private void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            SemanticValue semantics = e.Result.Semantics;
            string rawText = e.Result.Text;
            speechSynthesizer.Speak(rawText);

            if (rawText.Equals("Borrar comentario"))
            {
                this.dictationTextBox.Text = string.Empty;
            }

            IEnumerable<string> writeCommands = new List<string>() { "Escribir", "Añadir", "Dictar" };
            if (writeCommands.Any(s => rawText.Contains(s)))
            {
                speechRecognizer.UnloadAllGrammars();
                speechRecognizer.SpeechRecognized -= new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

                speechSynthesizer.Speak("Comienza dictado");

                speechRecognizer.LoadGrammar(new DictationGrammar());

                speechRecognizer.SpeechHypothesized += new EventHandler<SpeechHypothesizedEventArgs>(recognizer_SpeechHypothesized);
            }
        }

        private void EndDictation()
        {
            speechRecognizer.UnloadAllGrammars();
            speechRecognizer.SpeechHypothesized -= new EventHandler<SpeechHypothesizedEventArgs>(recognizer_SpeechHypothesized);

            speechSynthesizer.Speak("Fin dictado");

            speechRecognizer.LoadGrammar(grammar);

            speechRecognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
        }

        private void recognizer_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            if (e.Result.Confidence < 0.6)
            {
                return;
            }

            SemanticValue semantics = e.Result.Semantics;
            string rawText = e.Result.Text;

            IEnumerable<string> stopDictationCommands = new List<string>() { "fin dictado", "fin", "dictado" };
            if (stopDictationCommands.Any(s => rawText.Contains(s)))
            {
                this.EndDictation();

                return;
            }

            if (string.IsNullOrEmpty(this.dictationTextBox.Text))
            {
                this.dictationTextBox.Text = rawText;
            }
            else
            {
                this.dictationTextBox.Text += " " + rawText;
            }
        }
    }
}
