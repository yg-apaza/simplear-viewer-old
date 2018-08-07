[System.Serializable]
public class Resource {
    public string id;
    public string description;
    public string content;
    public string type;

    public Resource(string id, string description, string content, string type)
    {
        this.id = id;
        this.description = description;
        this.content = content;
        this.type = type;
    }
}
