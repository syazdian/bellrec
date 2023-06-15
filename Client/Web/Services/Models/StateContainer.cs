namespace Bell.Reconciliation.Frontend.Web.Models;

public class StateContainer : IStateContainer
{
    public List<BellSourceDto> bellSourcesCellPhone { get; set; } = new();
    public List<StaplesSourceDto> staplesSourcesCellPhone { get; set; } = new();
    public List<CompareBellStapleCellPhone> compareBellStapleCellPhone { get; set; } = new();

    public List<BellSourceDto> bellSourcesNonCellPhone { get; set; } = new();
    public List<StaplesSourceDto> staplesSourcesNonCellPhone { get; set; } = new();
    public List<CompareBellStapleNonCellPhone> compreBellStapleNonCellPhone { get; set; } = new();

    public FilterItemDto filterItemDto { get; set; } = new();

    public string User { get; set; } = "User";
}