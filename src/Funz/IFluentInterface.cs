using System;
using System.ComponentModel;

namespace Jwc.Funz
{
    /// <summary>
    /// Helper interface used to hide the base <see cref="object" />
    /// members from the fluent API to make for much cleaner
    /// Visual Studio intellisense experience.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFluentInterface
    {
        /// <summary />
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1716:IdentifiersShouldNotMatchKeywords",
            MessageId = "GetType")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        /// <summary />
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        /// <summary />
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();

        /// <summary />
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1720:IdentifiersShouldNotContainTypeNames",
            MessageId = "obj")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);
    }
}