using Ardalis.SmartEnum;

namespace Zxcvbn
{

    /// <summary>
    /// Represents the password score 0-4 (-1 for undefined), and string representation.
    /// </summary>
    public abstract class PasswordScore : SmartEnum<PasswordScore>
    {
        private PasswordScore(string name, int value) : base(name, value)
        {
        }

        /// <summary>
        /// Represents the password -1 (Undefined) score.
        /// </summary>
        public static readonly PasswordScore Undefined = new UndefinedPasswordScore();
        private sealed class UndefinedPasswordScore : PasswordScore
        {
            public UndefinedPasswordScore() : base(nameof(Undefined), -1)
            {
            }

            public override string ToString()
            {
                return PasswordScoreResources.ResourceManager.GetString(nameof(Undefined)) ?? PasswordScoreResources.Undefined;
            }
        }

        /// <summary>
        /// Represents the password 0 (Risky) score.
        /// </summary>
        public static readonly PasswordScore Risky = new RiskyPasswordScore();
        private sealed class RiskyPasswordScore : PasswordScore
        {
            public RiskyPasswordScore() : base(nameof(Risky), 0)
            {
            }

            public override string ToString()
            {
                return PasswordScoreResources.ResourceManager.GetString(nameof(Risky)) ?? PasswordScoreResources.Undefined;
            }
        }

        /// <summary>
        /// Represents the password 1 (Weak) score.
        /// </summary>
        public static readonly PasswordScore Weak = new WeakPasswordScore();
        private sealed class WeakPasswordScore : PasswordScore
        {
            public WeakPasswordScore() : base(nameof(Weak), 1)
            {
            }

            public override string ToString()
            {
                return PasswordScoreResources.ResourceManager.GetString(nameof(Weak)) ?? PasswordScoreResources.Undefined;
            }
        }

        /// <summary>
        /// Represents the password 2 (Fair) score.
        /// </summary>
        public static readonly PasswordScore Fair = new FairPasswordScore();
        private sealed class FairPasswordScore : PasswordScore
        {
            public FairPasswordScore() : base(nameof(Fair), 2)
            {
            }

            public override string ToString()
            {
                return PasswordScoreResources.ResourceManager.GetString(nameof(Fair)) ?? PasswordScoreResources.Undefined;
            }
        }

        /// <summary>
        /// Represents the password 3 (Good) score.
        /// </summary>
        public static readonly PasswordScore Good = new GoodPasswordScore();
        private sealed class GoodPasswordScore : PasswordScore
        {
            public GoodPasswordScore() : base(nameof(Good), 3)
            {
            }

            public override string ToString()
            {
                return PasswordScoreResources.ResourceManager.GetString(nameof(Good)) ?? PasswordScoreResources.Undefined;
            }
        }

        /// <summary>
        /// Represents the password 4 (Strong) score.
        /// </summary>
        public static readonly PasswordScore Strong = new StrongPasswordScore();
        private sealed class StrongPasswordScore : PasswordScore
        {
            public StrongPasswordScore() : base(nameof(Strong), 4)
            {
            }

            public override string ToString()
            {
                return PasswordScoreResources.ResourceManager.GetString(nameof(Strong)) ?? PasswordScoreResources.Undefined;
            }
        }
    }
}
