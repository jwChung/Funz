using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Jwc.Funz
{
    /// <summary>
    /// Helper interface used to hide the base <see cref="object" />
    /// members from the fluent API to make for much cleaner
    /// Visual Studio intelligence experience.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:ElementReturnValueMustBeDocumented", Justification = "Suppressing this rule is desirable.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Suppressing this rule is desirable.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1606:ElementDocumentationMustHaveSummaryText", Justification = "Suppressing this rule is desirable.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFluentInterface
    {
        /// <summary />
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "GetType", Justification = "Suppressing this rule is desirable.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Suppressing this rule is desirable.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        /// <summary />
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        /// <summary />
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();

        /// <summary />
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj", Justification = "Suppressing this rule is desirable.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);
    }
}