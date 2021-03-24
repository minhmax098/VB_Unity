using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organ
{
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public int id {get; set; }
    public string name {get; set; }
    public int gender {get; set; }
    public string description {get; set; }
    public int parent_id {get; set; }
    public int is_trial_available {get; set; }

    public Organ 
    (
        int _id, 
        string _name, 
        int _gender, 
        string _description, 
        int _parent_id, 
        int _is_trial_available
    )
    {
        id = _id; 
        name = _name; 
        gender = _gender; 
        description = _description; 
        parent_id = _parent_id; 
        is_trial_available = _is_trial_available;
    }

}
