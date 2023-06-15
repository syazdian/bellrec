namespace Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;

public interface IStateContainer
{
    public List<BellSourceDto> bellSourcesCellPhone { get; set; }
    public List<StaplesSourceDto> staplesSourcesCellPhone { get; set; }
    public List<CompareBellStapleCellPhone> compareBellStapleCellPhone { get; set; }

    public List<BellSourceDto> bellSourcesNonCellPhone { get; set; }
    public List<StaplesSourceDto> staplesSourcesNonCellPhone { get; set; }
    public List<CompareBellStapleNonCellPhone> compreBellStapleNonCellPhone { get; set; }

    public FilterItemDto filterItemDto { get; set; }

    public string User { get; set; }
}