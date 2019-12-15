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
            GrammarBuilder navigateUser = this.CreateNavigateUserGrammar();

            Choices choices = new Choices();
            choices.Add(navigateUser);

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
    }
}