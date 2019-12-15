namespace REcoSample.Profile
{
    using Microsoft.Extensions.DependencyInjection;
    using Persistence;
    using System;
    using System.Linq;
    using System.Speech.Recognition;

    public class ProfileGrammar
    {
        private readonly IServiceProvider serviceProvider;

        public ProfileGrammar(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        internal Grammar CreateGrammar()
        {
            GrammarBuilder navigateUserGrammar = this.CreateNavigateUserGrammar();
            GrammarBuilder hidePhotoGrammar = this.CreateHidePhotoGrammar();
            GrammarBuilder resizeFontGrammar = this.CreateResizeFontGrammar();

            Choices choices = new Choices();
            choices.Add(navigateUserGrammar);
            choices.Add(hidePhotoGrammar);
            choices.Add(resizeFontGrammar);

            Grammar grammar = new Grammar(choices);

            return grammar;
        }

        private GrammarBuilder CreateNavigateUserGrammar()
        {
            GrammarBuilder go = "Ir";
            GrammarBuilder navigate = "Navegar";

            Choices alternativesNavigate = new Choices(go, navigate);

            GrammarBuilder sentence = new GrammarBuilder(alternativesNavigate);

            GrammarBuilder to = "a";
            sentence.Append(to);

            Choices userChoice = new Choices();

            using (PersistenceContext context = this.serviceProvider.CreateScope().ServiceProvider.GetService<PersistenceContext>())
            {
                foreach (User user in context.Users)
                {
                    SemanticResultValue userChoiceResultValue = new SemanticResultValue(user.Name, user.Id.ToString());
                    userChoice.Add(new GrammarBuilder(userChoiceResultValue));
                }
            }

            SemanticResultKey userChoiceResultKey = new SemanticResultKey("userIdToNavigate", userChoice);
            sentence.Append(new GrammarBuilder(userChoiceResultKey));

            return sentence;
        }

        private GrammarBuilder CreateHidePhotoGrammar()
        {
            Choices optionsChoice = new Choices();

            SemanticResultValue hidePhotoResultValue = new SemanticResultValue("Ocultar", value: false);
            optionsChoice.Add(hidePhotoResultValue);

            SemanticResultValue showPhotoResultValue = new SemanticResultValue("Mostrar", value: true);
            optionsChoice.Add(showPhotoResultValue);

            SemanticResultKey optionsResultKey = new SemanticResultKey("photoEnabled", optionsChoice);

            GrammarBuilder sentence = new GrammarBuilder(optionsResultKey);

            GrammarBuilder photo = "foto";
            sentence.Append(photo);

            return sentence;
        }

        private GrammarBuilder CreateResizeFontGrammar()
        {
            Choices optionsChoice = new Choices();

            SemanticResultValue increaseFontResultValue = new SemanticResultValue("Aumentar", value: true);
            optionsChoice.Add(increaseFontResultValue);

            SemanticResultValue reduceFontResultValue = new SemanticResultValue("Reducir", value: false);
            optionsChoice.Add(reduceFontResultValue);

            SemanticResultKey optionsResultKey = new SemanticResultKey("increaseFontSize", optionsChoice);

            GrammarBuilder sentence = new GrammarBuilder(optionsResultKey);

            GrammarBuilder letter = "letra";
            GrammarBuilder font = "fuente";
            GrammarBuilder letterSize = "tamaño de letra";
            GrammarBuilder fontSize = "tamaño de fuente";

            Choices alternativesNavigate = new Choices(letter, font, letterSize, fontSize);

            sentence.Append(new GrammarBuilder(alternativesNavigate));

            return sentence;
        }
    }
}