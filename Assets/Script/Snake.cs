using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Snake : MonoBehaviour {

	// Did the snake eat something?
	bool ate = false;

	//Did user died?
	bool isDied = false;

	// Tail Prefab
	public GameObject tailPrefab;

    int ScoreVal = 0;
	// Current Movement Direction
	// (by default it moves to the right)
	public Vector2 dir = Vector2.right;

	// Keep Track of Tail
	List<Transform> tail = new List<Transform>();

     public GameObject up;
     public GameObject right;
     public GameObject left;
     public GameObject down;
    public GameObject ScoreDisp;

     // Display control
     DispKey dispUp;
     DispKey dispRight;
     DispKey dispLeft;
     DispKey dispDown;
     DispKey score;

     BlinkEffect blinkUp;
     BlinkEffect blinkRight;
     BlinkEffect blinkLeft;
     BlinkEffect blinkDown;


    //key seed
    string rightKey;
    string upKey;
    string leftKey;
    string downKey;

    public float speed;
    BlinkEffect blinkSnake;


    // Use this for initialization
     void Start () {
        //Refresh rate
         speed = 0.3f;
         dispDown = down.GetComponent<Text>().GetComponent<DispKey>();
         dispUp = up.GetComponent<Text>().GetComponent<DispKey>();
         dispRight = right.GetComponent<Text>().GetComponent<DispKey>();
         dispLeft = left.GetComponent<Text>().GetComponent<DispKey>();
         score = ScoreDisp.GetComponent<Text>().GetComponent<DispKey>();

        rightKey = RandControl(rightKey);
        downKey = RandControl(downKey);
        leftKey = RandControl(leftKey);
        upKey = RandControl(upKey);

        if (rightKey == leftKey || rightKey == downKey || rightKey == upKey) rightKey = RandControl(rightKey);
         if (downKey == rightKey || downKey == leftKey || downKey == upKey) downKey = RandControl(downKey);
         if (leftKey == rightKey || leftKey == downKey || leftKey == upKey) leftKey = RandControl(leftKey);
         if (upKey == rightKey || upKey == downKey || upKey == leftKey) upKey = RandControl(upKey);

         dispRight.NewKey(rightKey);
         dispLeft.NewKey(leftKey);
         dispDown.NewKey(downKey);
         dispUp.NewKey(upKey);

         blinkUp = up.GetComponentInChildren<BlinkEffect>();
         blinkRight = right.GetComponentInChildren<BlinkEffect>();
         blinkLeft = left.GetComponentInChildren<BlinkEffect>();
         blinkDown = down.GetComponentInChildren<BlinkEffect>();
         blinkSnake = GetComponent<BlinkEffect>();
        
         InvokeRepeating("Move", speed, speed);
    }
    // Update is called once per frame
    string RandControl(string key)
    {
        char c = (char)('a' + Random.Range(0, 26));
        key = c.ToString();
        Debug.Log(key);
        return key;
    }
    void Update () {
        score.NewKey(ScoreVal.ToString());
        if (!isDied)
        {
            if (Input.inputString == rightKey)
            {
                dir = Vector2.right;
                blinkRight.getBlink(1);
                rightKey = RandControl(rightKey);
                if (rightKey == downKey || rightKey == leftKey || rightKey == upKey) rightKey = RandControl(rightKey);
                dispRight.NewKey(rightKey);
            }
            else if (Input.inputString == downKey)
            {
                dir = -Vector2.up;    // '-up' means 'down'
                blinkDown.getBlink(1);
                downKey = RandControl(downKey);
                if (downKey == rightKey || downKey == leftKey || downKey == upKey) downKey = RandControl(downKey);
                dispDown.NewKey(downKey);
            }
            else if (Input.inputString == leftKey)
            {
                dir = -Vector2.right; // '-right' means 'leftKey'
                blinkLeft.getBlink(1);
                leftKey = RandControl(leftKey);
                if (leftKey == rightKey || leftKey == downKey || leftKey == upKey) leftKey = RandControl(leftKey);
                dispLeft.NewKey(leftKey);
            }
            else if (Input.inputString == upKey)
            {
                dir = Vector2.up;
                blinkUp.getBlink(1);
                upKey = RandControl(upKey);
                if (upKey == rightKey || upKey == downKey || upKey == leftKey) upKey = RandControl(upKey);
                dispUp.NewKey(upKey);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.R))
            {
                //clear the tail
                tail.Clear();

                //reset to origin
                transform.position = new Vector3(0, 0, 0);

                //make snake alive
                isDied = false;
            }
        }
    }

    void Move() {
		if (!isDied) {
			// Save current position (gap will be here)
			Vector2 v = transform.position;

			// Move head into new direction (now there is a gap)
			transform.Translate (dir);

			// Ate something? Then insert new Element into gap
			if (ate) {
				// Load Prefab into the world
				GameObject g = Instantiate(tailPrefab,v, Quaternion.identity);

				// Keep track of it in our tail list
				tail.Insert (0, g.transform);
                if(speed > 0.1) speed -= 0.01f;
                Debug.Log(speed);
                // Reset the flag
                ate = false;
			} else if (tail.Count > 0) {	// Do we have a Tail?
					// Move last Tail Element to where the Head was
					tail.Last ().position = v;

					// Add to front of list, remove from the back
					tail.Insert (0, tail.Last ());
					tail.RemoveAt (tail.Count - 1);
			}
		}
	}

    void OnTriggerEnter2D(Collider2D coll) {
        // Food?
        if (coll.name.StartsWith("Food")) {
            // Get longer in next Move call

            ate = true;
            Destroy(coll.gameObject);
            // Remove the Food
            blinkSnake.getBlink(5);
            ScoreVal++;
            
		} else { 	
			isDied = true;
		}
	}
}
 