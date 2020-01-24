using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{   Vector2 StartPos; //posição para ser usada na movimentação da espada com o dedo
    public float DeltaX; // Delta do x da posição entre um frame e outro utilizado para movimentar o objeto de acordo com a movimentacao do
    public float DeltaY;// Delta do y da posição entre um frame e outro utilizado para movimentar o objeto de acordo com a movimentacao do dedo/mouse
    bool mouseDown = false; //utilizado para controlar quando o dedo/mouse está tocando a tela
    Vector3 ScreenToWorld; //Variável para facilitar a escrita do ScreentoWorld na hora de tocar
    Vector3 ScreenToVP;
    Vector3 StartRot;
    public float velocidade = 0.2f;
    Quaternion rotacaoInicial;
    bool attack = false;
    // Update is called once per frame
    public float RotX;
    public float RotY;
    public float TRotX;
    public float TRotZ;

    public float i;
    void Start() {
        rotacaoInicial = transform.rotation;
    }
    void Update()
    {//Faz a leitura do input do mouse em World points, fiz essa variavel para facilitar a reescrita
        ScreenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));// (Input.mousePosition);
        ScreenToVP = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (!attack){
            //transform.rotation = Quaternion.Lerp(transform.rotation, Camera.main.transform.rotation, velocidade);
            iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(0, 0, 0), 
                    "time", 3,
                    "islocal", true,
                    "easetype", iTween.EaseType.linear));
            }
        if (Input.GetMouseButtonDown(0)){
            mouseDown = true; //ativa a variavel para iniciar a leitura do movimento do dedo)
            StartPos = new Vector3(ScreenToWorld.x,ScreenToWorld.y, 0);
            StartRot = new Vector3(ScreenToVP.x, ScreenToVP.y, 0);
        }

        if (Input.GetMouseButtonUp(0)){
            mouseDown = false;
        }

        if (mouseDown && !attack){ //se estiver tocando a tela:
            DeltaX = ScreenToWorld.x - StartPos.x; //variaçao entre a posição atual do dedo no eixo x e a posição do frame anterior (qanto o dedo movimentou)
            DeltaY = ScreenToWorld.y - StartPos.y;// o mesmo no eixo y
            float ClampX = Mathf.Clamp(transform.position.x + DeltaX, -1f, 7.5f); 
            float ClampY = Mathf.Clamp(transform.position.y + DeltaY, 3f, 14f); 
            transform.position = new Vector3(ClampX, ClampY, transform.position.z); //movimenta o objeto de acordo com a movimentacao do dedo
            StartPos = new Vector3(ScreenToWorld.x,ScreenToWorld.y, 0); //faz a leitura do toque do frame atual para fazer a comparacao com o proximo
            
            RotX = ScreenToVP.x - StartRot.x;
            RotY = ScreenToVP.y - StartRot.y;
            iTween.RotateAdd(gameObject, (new Vector3(-RotY*200, 0, RotX*50)), 0);
            //TRotX = transform.localRotation.eulerAngles.x - RotY*150;
            //TRotZ = transform.localRotation.eulerAngles.z + RotX*30;
            
            float ClampRotX = Mathf.Clamp((TRotX <= 180) ? TRotX : -(360 - TRotX), -40, 90);
            float ClampRotZ = Mathf.Clamp((TRotZ <= 180) ? TRotZ : -(360 - TRotZ), -10, 10);            
            //transform.localRotation = Quaternion.Euler(new Vector3(ClampRotX, transform.localRotation.eulerAngles.y, ClampRotZ));
            StartRot = new Vector3(ScreenToVP.x, ScreenToVP.y, 0);
            //if(TRotX > 45 && RotY < 0){attack = true; Attack();}
        } 

        void Attack(){
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(90, 
                                transform.localRotation.eulerAngles.y, 
                                transform.localRotation.eulerAngles.z)),
                                1);
            
            attack = false;
            Debug.Log("attack!");
        }
        
    }
}
