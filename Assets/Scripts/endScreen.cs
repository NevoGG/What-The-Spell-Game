using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endScreen : MonoBehaviour
{
    
    [SerializeField] GameObject mainCam;
    [SerializeField] private List<Vector3> positions;

    // Start is called before the first frame update
    void Start()
    {
        mainCam.transform.position = new Vector3(transform.position.x, transform.position.y,
            mainCam.transform.position.z);
    }

    public void EndGame(List<Player> rankList)
    {
        foreach (var player in rankList)
        {
            if (!player.gameObject.activeSelf)
            {
                player.gameObject.SetActive(true);
                player.TurnToBlob();
            }
        }

        for (int i = 0; i < rankList.Count; i++)
        {
            rankList[i].MoveToPosition(positions[i]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
