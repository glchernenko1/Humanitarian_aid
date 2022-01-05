using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BattledoreMove : MonoBehaviour
{
    // Start is called before the first frame update
    
    const int maxLevel = 30;
    [Range(1, maxLevel)]
    public int level = 1;
    public float ballVelocityMult = 0.02f;
    public GameObject bluePrefab;
    public GameObject redPrefab;
    public GameObject greenPrefab;
    public GameObject yellowPrefab;
    public GameObject ballPrefab;
    public GameDataScript gameData;
    public GameObject redOorX;
    public GameObject convas;
    public GameObject gun1;
    public GameObject gun2;
    
   
    
    
    
    AudioSource audioSrc;
    public AudioClip pointSound;
    
    int requiredPointsToBall
    { get { return 400 + (level - 1) * 20; } }
    
    static bool gameStarted = false;
    
    static Collider2D[] colliders = new Collider2D[50];
    static ContactFilter2D contactFilter = new ContactFilter2D();
    
    void CreateBlocks(GameObject prefab, float xMax, float yMax,
        int count, int maxCount)
    {
        if (count > maxCount)
            count = maxCount;
        for (int i = 0; i < count; i++)
        for (int k = 0; k < 20; k++)
        {
            var obj = Instantiate(prefab,
                new Vector3((Random.value * 2 - 1) * xMax,
                    Random.value * yMax, 0),
                Quaternion.identity);
            if (obj.GetComponent<Collider2D>()
                .OverlapCollider(contactFilter.NoFilter(), colliders) == 0)
                break;
            Destroy(obj);
        }
    }

    int randomBonus()
    {
        var rnd = Random.Range(0f,1f);
        
        if (rnd < gameData.bonusNot)
            return -1;
        if (rnd < gameData.bonusNot+ gameData.bonusPlus)
            return 0;
        if (rnd < gameData.bonusNot+ gameData.bonusPlus + gameData.bonusFire)
            return 1;
        if (rnd < gameData.bonusNot+ gameData.bonusPlus + gameData.bonusFire + gameData.bonusSteel)
            return 2;
        if (rnd < gameData.bonusNot+ gameData.bonusPlus + gameData.bonusFire + gameData.bonusSteel+ gameData.bonusNorm)
            return 3;
        if (rnd < gameData.bonusNot+ gameData.bonusPlus + gameData.bonusFire + gameData.bonusSteel+ gameData.bonusNorm+gameData.bonusGun)
            return 4;
        if (rnd < gameData.bonusNot+ gameData.bonusPlus + gameData.bonusFire + gameData.bonusSteel+ gameData.bonusNorm+gameData.bonusGun+gameData.bonusCannon)
            return 5;
        if (rnd < gameData.bonusNot+ gameData.bonusPlus + gameData.bonusFire + gameData.bonusSteel+ gameData.bonusNorm+gameData.bonusGun+gameData.bonusCannon+gameData.bonusBomb)
            return 6;
        return 7;
    }
    
    void CreateBlocksBonus(GameObject prefab, float xMax, float yMax,
        int count, int maxCount)
    {
        if (count > maxCount)
            count = maxCount;
        for (int i = 0; i < count; i++)
        for (int k = 0; k < 20; k++)
        {
            var obj = Instantiate(prefab,
                new Vector3((Random.value * 2 - 1) * xMax,
                    Random.value * yMax, 0),
                Quaternion.identity);
            if (obj.GetComponent<Collider2D>()
                .OverlapCollider(contactFilter.NoFilter(), colliders) == 0)
            {
                obj.GetComponent<BlockBonus>().bonusNumber = randomBonus(); 
                break;
            }

            Destroy(obj);
        }
    }
    
    
    public void CreateBalls()
    {
        Debug.Log("ПУУУУШШШКА" + gun1.GetComponent<Wepon>().isActivity);
        if (gun1.GetComponent<Wepon>().isActivity || gun2.GetComponent<Wepon>().isActivity) return;
        int count = 2;
        if (gameData.balls == 1)
            count = 1;

        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(ballPrefab);
            var ball = obj.GetComponent<BallScript>();
            ball.ballInitialForce += new Vector2(10 * i, 0);
            ball.ballInitialForce *= 1 + level * ballVelocityMult;
            ball.playerObj = this.gameObject;
            obj.transform.position = new Vector3(0, -4.1f, 0);
        }
    }
    
    void StartLevel()
    {

        var tmp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +"/My Games/Arkanoid" + "//xmlLevel" + (gameData.level - 1) + ".xml";
        if (!File.Exists(tmp))
        {
            SetBackground();
            var yMax = Camera.main.orthographicSize * 0.8f;
            var xMax = Camera.main.orthographicSize * Camera.main.aspect * 0.85f;
            CreateBlocks(bluePrefab, xMax, yMax, level, 8);
            CreateBlocks(redPrefab, xMax, yMax, 1 + level, 10);
            CreateBlocksBonus(greenPrefab, xMax, yMax, 1 + level, 12);
            CreateBlocks(yellowPrefab, xMax, yMax, 2 + level, 15);
            CreateBlocks(redOorX, xMax, yMax, 1 + level, 4);
            CreateBalls();
        }
        else
        {
          XmlTextReader reader = new XmlTextReader(tmp);
            reader.WhitespaceHandling = WhitespaceHandling.None;
            string load = "";
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                    load = reader.Value;
            }
            string[] np = load.Split('|');
            Debug.Log(np[0].Length);
            string[] position = np[1].Split('+');
            for (int i = 0; i < np[0].Length; i++)
            {
                string[] vecStr = position[i].Split(':');
                Vector3 vec = new Vector3(float.Parse(vecStr[0]), float.Parse(vecStr[1]), 0);
                GameObject obj = new GameObject();
                switch (np[0][i])
                {
                    case ('R'):
                        obj = Instantiate(redPrefab);
                        break;
                    case ('G'):
                        obj = Instantiate(greenPrefab);
                        obj.GetComponent<BlockBonus>().bonusNumber = randomBonus();
                        break;
                    case ('B'):
                        obj = Instantiate(bluePrefab);
                        break;
                    case ('Y'):
                        obj = Instantiate(yellowPrefab);
                        break;
                    case ('S'):
                        obj = Instantiate(redOorX);
                        break;
                }
                obj.transform.position = vec;
            }
            Debug.Log("ffffffff");
            CreateBalls();
        }
    }
    
    
    string OnOff(bool boolVal)
    {
        return boolVal ? "on" : "off";
    }
    
    void OnGUI()
    {
        GUI.Label(new Rect(5, 4, Screen.width - 10, 100),
            string.Format(
                "<color=yellow><size=30>Level <b>{0}</b> Balls <b>{1}</b>"+
                " Score <b>{2}</b></size></color>",
                gameData.level, gameData.balls, gameData.points));
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperRight;
        GUI.Label(new Rect(5, 14, Screen.width - 10, 100),
            string.Format("<color=yellow><size=20><color=white>Space</color>-pause {0}" +
                          " <color=white>N</color>-new" +
                          " <color=white>J</color>-jump" +
                          " <color=white>M</color>-music {1}" +
                          " <color=white>S</color>-sound {2}" +
                          " <color=white>Esc</color>-exit</size></color>",
                OnOff(Time.timeScale > 0), OnOff(!gameData.music),
                OnOff(!gameData.sound)), style);

    }
    
    
    void SetBackground()
    {
        var bg = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        bg.sprite = Resources.Load(level.ToString("d2"),
            typeof(Sprite)) as Sprite;
    }
    
    
    

    
    public void BallDestroyed()
    {
        if ( GameObject.FindGameObjectsWithTag("Ball").Length <= 0)
            if (gameData.balls > 0  ) //gun2.active
                CreateBalls();
            else
            {
                for (int i = 0; i < gameData.bestPlayer.Length; ++i)
                {
                    if (int.Parse(gameData.bestPlayer[i].Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)[1])<
                        gameData.points)
                    {
                        for (int j = gameData.bestPlayer.Length-1; j > i ; --j)
                            gameData.bestPlayer[j]= gameData.bestPlayer[j-1];
                        gameData.bestPlayer[i] = gameData.NamePlayer + "         " + gameData.points;
                        break;
                    }
                        
                }
                
                gameData.Reset();
                SceneManager.LoadScene("MainScene");
            }
    }

    public void CheckBalls()
    {
        gameData.balls--;
        Invoke("BallDestroyed", 0.2f);
    }

    public void BlockDestroyeds()
    {
        Debug.Log("Block1" + GameObject.FindGameObjectsWithTag("Block").Length );
        Debug.Log("Block2" + GameObject.FindGameObjectsWithTag("RedBlock").Length  );
        if (GameObject.FindGameObjectsWithTag("Block").Length == 0 && GameObject.FindGameObjectsWithTag("RedBlock").Length == 0)
        {
            if (level < maxLevel)
            {
                gameData.level++;
                SceneManager.LoadScene("MainScene");
            }
            else
            {
                gameData.isFinsh = true;
                SceneManager.LoadScene("MenuScense");
            }
        }

    }
    
    public void BlockDestroyed(int points)
    {
        gameData.points += points;
        if (gameData.sound)
            audioSrc.PlayOneShot(pointSound);
        gameData.pointsToBall += points;
        if (gameData.pointsToBall >= requiredPointsToBall)
        {
            gameData.balls++;
            gameData.pointsToBall -= requiredPointsToBall;
        }
        Invoke("BlockDestroyeds", 0.3f);
    }
    
    
    void SetMusic()
    {
        if (gameData.music)
            audioSrc.Play();
        else
            audioSrc.Stop();
    }

    
    
    void Start()
    {
        gameData.isFinsh = false;
        audioSrc = Camera.main.GetComponent<AudioSource>();
        SetMusic();
        Cursor.visible = false;
        if (!gameStarted)
        {
            gameStarted = true;
            if (gameData.resetOnStart)
                gameData.Load();
        }
        level = gameData.level;
        
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))

            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                convas.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                Cursor.visible = false;
                convas.SetActive(false);
            }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
                        Application.Quit();
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #endif
        } 
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            gameData.music = !gameData.music;
            SetMusic();
        }

        if (Input.GetKeyDown(KeyCode.S))
            gameData.sound = !gameData.sound;

        if (Input.GetKeyDown(KeyCode.N))
        {
            gameData.Reset();
            SceneManager.LoadScene("MainScene");
        }
        
        if (Time.timeScale > 0)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var pos = transform.position;
            pos.x = mousePos.x;
            transform.position = pos;
            

        }
    }

    public void Exit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void REBOT()
    {
        gameData.Reset();
        SceneManager.LoadScene("MainScene");
    }
    
    
    void OnApplicationQuit()
    {
        gameData.Save();
    }

    
}
