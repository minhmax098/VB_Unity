using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TagHandler : MonoBehaviour
{
    private static TagHandler instance; 
    public static TagHandler Instance
    {
        get {
            if(instance == null)
            {
                instance = FindObjectOfType<TagHandler>();
            }
            return instance; 
        }
    }

    public TextAsset textJson; 

    public List<GameObject> addedTags = new List<GameObject>();
    [System.Serializable]
    public class Point 
    {
        public Vector3 coordinate 
        {
            get; 
            set; 
        }
        public Vector3 direction 
        {
            get; 
            set; 
        }
        public float angle 
        {
            get; 
            set; 
        }
        public Point (Vector3 coordinate, Vector3 direction)
        {
            this.coordinate = coordinate; 
            this.direction = direction; 
        }
    }

    [System.Serializable]
    public class Tag 
    {
        public string name {get; set; }
        public string description {get; set; }

        public Point point { get; set; }
        public Vector3 tag { get; set; }

        public Tag[] child {get; set; }

        public Tag (string name, string description, Point point, Vector3 tag, Tag[] child)
        {
            this.name = name; 
            this.description = description; 
            this.point = point; 
            this.tag = tag; 
            this.child = child; 
        }

        
    }

    [System.Serializable]
    public class Atlas 
    {
        public Tag[] tags; 
    }

    public Atlas atlas = new Atlas();  

    // Start is called before the first frame update
    void Start()
    {
        // loadTags()
        initAtlas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initAtlas()
    {
        atlas.tags = new Tag[]
        {

        };
    }

    public void loadTags()
    {
        int i=0; 
        foreach(Tag tag in atlas.tags)
        {
            addedTags.Add(Instantiate(Resource.Load("tag") as GameObject)); 
            adjustTag(i, tag); 
            i++; 

        }
    }

    
}
