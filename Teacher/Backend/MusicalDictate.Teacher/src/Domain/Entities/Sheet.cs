using Domain.Common;

namespace Domain.Entities;

public class Sheet : BaseAuditableEntity
{
    private Sheet() { }
    public byte[] MusicXml { get; private set; }

    public ICollection<StudentSolution> StudentSolutions { get; private set; } = new List<StudentSolution>();

    public static Sheet Create(byte[] musicXml)
    {
        return new Sheet {MusicXml = musicXml};
    }
}