using System.Collections.Generic;

namespace Zxcvbn
{
    /// <summary>
    /// Represents feedback that can be presented to the user.
    /// </summary>
    public class FeedbackItem
    {
        /// <summary>
        /// Gets the default feedback, not precomputed to take into account CultureInfo changes.
        /// </summary>
        public static FeedbackItem Default {
            get {
                return new()
                {
                    Warning = string.Empty,
                    Suggestions = new[]
                    {
                        FeedbackResources.Default1,
                        FeedbackResources.Default2,
                    },
                };
            }
        }

        /// <summary>
        /// Gets the list of suggestions that can be presented.
        /// </summary>
        public IList<string> Suggestions { get; internal set; }

        /// <summary>
        /// Gets the warning that should be the headline for the user.
        /// </summary>
        public string Warning { get; internal set; }
    }
}
