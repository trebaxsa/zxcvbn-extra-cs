using System.Collections.Generic;
using System.Linq;
using Zxcvbn.Matcher.Matches;

namespace Zxcvbn
{
    /// <summary>
    /// Generates feedback based on the match results.
    /// </summary>
    internal static class Feedback
    {
        /// <summary>
        /// Gets feedback based on the provided score and matches.
        /// </summary>
        /// <param name="score">The score to assess.</param>
        /// <param name="sequence">The sequence of matches to assess.</param>
        /// <param name="userInputsWarningMessage"> The warning message for user data matches.</param>
        /// <returns>Any warnings and suggestiongs about the password matches.</returns>
        public static FeedbackItem GetFeedback(int score, IEnumerable<Match> sequence, string userInputsWarningMessage)
        {
            if (!sequence.Any())
                return FeedbackItem.Default;

            if (score > 2)
            {
                return new FeedbackItem
                {
                    Warning = string.Empty,
                    Suggestions = new List<string>(),
                };
            }

            var longestMatch = sequence.OrderBy(c => c.Token.Length).Last();

            var feedback = GetMatchFeedback(longestMatch, sequence.Count() == 1, userInputsWarningMessage);
            var extraFeedback = FeedbackResources.ExtraFeedback;

            if (feedback != null)
            {
                feedback.Suggestions.Insert(0, extraFeedback);
            }
            else
            {
                feedback = new FeedbackItem
                {
                    Warning = string.Empty,
                    Suggestions = new List<string> { extraFeedback },
                };
            }

            return feedback;
        }

        private static FeedbackItem GetDictionaryMatchFeedback(DictionaryMatch match, bool isSoleMatch, string userInputsWarningMessage)
        {
            var warning = string.Empty;

            if (match.DictionaryName == "passwords")
            {
                if (isSoleMatch && !match.L33t && !match.Reversed)
                {
                    if (match.Rank <= 10)
                        warning = FeedbackResources.Top10;
                    else if (match.Rank <= 100)
                        warning = FeedbackResources.Top100;
                    else
                        warning = FeedbackResources.VeryCommon;
                }
                else if (match.GuessesLog10 <= 4)
                {
                    warning = FeedbackResources.SimilarToCommon;
                }
            }
            else if (match.DictionaryName == "english" && isSoleMatch)
            {
                warning = FeedbackResources.WordItselft;
            }
            else if (match.DictionaryName == "surnames" || match.DictionaryName == "male_names" || match.DictionaryName == "female_names")
            {
                if (isSoleMatch)
                    warning = FeedbackResources.NamesThemselves;
                else
                    warning = FeedbackResources.CommonNames;
            }
            else if (match.DictionaryName == "user_inputs")
            {
                warning = userInputsWarningMessage;
            }

            var suggestions = new List<string>();
            var word = match.Token;
            if (char.IsUpper(word[0]))
                suggestions.Add(FeedbackResources.Capitalization);
            else if (word.All(c => char.IsUpper(c)) && word.ToLower() != word)
                suggestions.Add(FeedbackResources.AllUppercas);

            if (match.Reversed && match.Token.Length >= 4)
                suggestions.Add(FeedbackResources.ReversedWords);
            if (match.L33t)
                suggestions.Add(FeedbackResources.Substitutions);

            return new FeedbackItem
            {
                Suggestions = suggestions,
                Warning = warning,
            };
        }

        private static FeedbackItem GetMatchFeedback(Match match, bool isSoleMatch, string userInputsWarningMessage)
        {
            switch (match.Pattern)
            {
                case "dictionary":
                    return GetDictionaryMatchFeedback(match as DictionaryMatch, isSoleMatch, userInputsWarningMessage);

                case "spatial":
                    return new FeedbackItem
                    {
                        Warning = (match as SpatialMatch).Turns == 1 ? FeedbackResources.KeyRows : FeedbackResources.ShortKeyboardPatterns,
                        Suggestions = new List<string>
                        {
                            FeedbackResources.UseLongerPattern,
                        },
                    };

                case "repeat":
                    return new FeedbackItem
                    {
                        Warning = (match as RepeatMatch).BaseToken.Length == 1 ? FeedbackResources.SingleRepeats : FeedbackResources.GroupRepeats,
                        Suggestions = new List<string>
                        {
                            FeedbackResources.AvoidRepeats,
                        },
                    };

                case "regex":
                    if ((match as RegexMatch).RegexName == "recent_year")
                    {
                        return new FeedbackItem
                        {
                            Warning = FeedbackResources.RecentYears,
                            Suggestions = new List<string>
                            {
                                FeedbackResources.AvoidRecentYears,
                                FeedbackResources.AvoidAssociatedYears,
                            },
                        };
                    }

                    break;

                case "date":
                    return new FeedbackItem
                    {
                        Warning = FeedbackResources.Dates,
                        Suggestions = new List<string>
                        {
                            FeedbackResources.AvoidAssociatedDates,
                        },
                    };
            }

            return null;
        }
    }
}
