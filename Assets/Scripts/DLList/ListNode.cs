public class ListNode
{
    public ListNode nextNode;
    public ListNode prevNode;
    public int Amount;
    
    private BaseStickerClass _sticker;
    public BaseStickerClass Sticker 
    {
        get => _sticker;

        set
        {
            if(value != null)
            {
                _sticker = value;
            }
        }
    }
    private string _name;
    public string Name { get => _name; set{_name = value;} }
}
