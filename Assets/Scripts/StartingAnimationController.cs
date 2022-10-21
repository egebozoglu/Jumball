using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingAnimationController : MonoBehaviour
{
    public static StartingAnimationController instance;

    public List<GameObject> startingBalls = new List<GameObject>();

    float rate = 0f;
    float time = 0.2f;

    string animationName = "BlueBall";
    int index = 0;

    bool animationActive = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        startingBalls[index].GetComponent<Animation>().Play(animationName);
    }

    private void Update()
    {
        Animation();
    }

    public void Animation()
    {
        if (!animationActive)
        {
            rate += Time.deltaTime;
            if (rate > time)
            {
                rate = 0f;
                index++;
                switch (index)
                {
                    case 0:
                        animationName = "BlueBall";
                        break;
                    case 1:
                        animationName = "GreenBall";
                        break;
                    case 2:
                        animationName = "OrangeBall";
                        break;
                    case 3:
                        animationName = "PinkBall";
                        break;
                    case 4:
                        animationName = "PurpleBall";
                        break;
                }
                startingBalls[index].GetComponent<Animation>().Play(animationName);
                if (index == 4)
                {
                    animationActive = true;
                }
            }
        }
    }
}
