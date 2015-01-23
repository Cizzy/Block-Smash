using UnityEngine;
using System.Collections;

public class Build_Wall : MonoBehaviour {

	public int difficulty=10;
	//public Camera myCam;

	void Start () {
		//scale screen
		Screen.orientation = ScreenOrientation.Portrait;

		//scale camera
		Camera.main.orthographicSize = Screen.height/2;

		//Center Camera
		Vector3 temp = Camera.main.transform.position;
		temp.x = Screen.width / 2;
		temp.y = Screen.height / 2;
		Camera.main.transform.position = temp;

		//Create Block Prefab (Unbroken)
		GameObject block=new GameObject();
		//MeshRenderer meshrenderer=block.GetComponent<MeshRenderer>();
		Mesh blockmesh = new Mesh ();
		blockmesh.name="Block_Mesh";
		blockmesh.Clear ();//clear vertex,triangle data, etc.
		blockmesh.vertices=new Vector3[4]{
			new Vector3 (0, 0, 20),
			new Vector3 (Screen.width/difficulty, 0, 20),
			new Vector3 (0, Screen.height/difficulty, 20),
			new Vector3 (Screen.width/difficulty, Screen.height/difficulty, 20)
		};
		blockmesh.uv = new Vector2[4]{
			new Vector2 (0, 0),
			new Vector2 (1, 0),
			new Vector2 (0, 1),
			new Vector2 (1, 1)
		};
		blockmesh.triangles = new int[6]{
			0,1,2,1,3,2
		};
		blockmesh.RecalculateNormals ();
		MeshFilter blockmeshfilter = (MeshFilter)block.gameObject.AddComponent(typeof(MeshFilter));
		MeshRenderer blockmeshrenderer = (MeshRenderer)block.gameObject.AddComponent(typeof(MeshRenderer));
		//blockmeshrenderer.enabled = false;//get rid of that fucking annoying pink bastard
		blockmeshfilter.mesh = blockmesh;

		//create colour selection array
		int colorcount = 7;//with higher difficulties, colorcount can be increased to include more colours, (max is 7 for now)
		Color[] colours=new Color[7]{
			Color.white,
			Color.red,
			Color.blue,
			Color.green,
			Color.yellow,
			Color.magenta,
			Color.cyan,
		};

		//default material to apply to blocks
		Material tmpmaterial = new Material(Shader.Find ("Diffuse"));
		tmpmaterial.color= Color.black;

		//Build Wall
		float blockwidth = Screen.width / difficulty;
		float blockheight = Screen.height / difficulty;

		//blockmeshrenderer.enabled = true;

		for (int i=0; i<difficulty; i++) {
			for(int j=0; j<difficulty; j++){

				//spawn block
				GameObject newBlock = (GameObject)Instantiate(block,new Vector3(i*blockwidth,j*blockheight,-20.0f),Quaternion.identity);
				newBlock.renderer.material.color=colours[Random.Range(0,colorcount)];
				BoxCollider2D newcollider=newBlock.AddComponent<BoxCollider2D>();
				newcollider.transform.position=newBlock.transform.position;
				newcollider.size=newBlock.renderer.bounds.size;
			}
		}
	}
}
