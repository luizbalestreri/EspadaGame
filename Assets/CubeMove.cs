using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{   Vector2 StartPos; //posição para ser usada na movimentação da espada com o dedo
    float DeltaX; // Delta do x da posição entre um frame e outro utilizado para movimentar o objeto de acordo com a movimentacao do
    float DeltaY;// Delta do y da posição entre um frame e outro utilizado para movimentar o objeto de acordo com a movimentacao do dedo/mouse
    bool mouseDown = false; //utilizado para controlar quando o dedo/mouse está tocando a tela
    Vector3 ScreenToWorld; //Variável para facilitar a escrita do ScreentoWorld na hora de tocar
    Vector3 ScreenToVP;
    Vector3 StartRot;
    public float velocidade = 1f;
    
    Quaternion rotacaoInicial;
    // Update is called once per frame
    void Start() {
        rotacaoInicial = transform.rotation;    
    }
    void Update()
    {//Faz a leitura do input do mouse em World points, fiz essa variavel para facilitar a reescrita
        ScreenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));// (Input.mousePosition);
        ScreenToVP = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, Camera.main.transform.rotation, velocidade);
        if (Input.GetMouseButtonDown(0)){
            mouseDown = true; //ativa a variavel para iniciar a leitura do movimento do dedo)
            StartPos = new Vector3(ScreenToWorld.x,ScreenToWorld.y, 0);
            StartRot = new Vector3(ScreenToVP.x, ScreenToVP.y, 0);
        }

        if (Input.GetMouseButtonUp(0)){
            mouseDown = false;
        }

        if (mouseDown == true){ //se estiver tocando a tela:
            DeltaX = ScreenToWorld.x - StartPos.x; //variaçao entre a posição atual do dedo no eixo x e a posição do frame anterior (qanto o dedo movimentou)
            DeltaY = ScreenToWorld.y - StartPos.y;// o mesmo no eixo y
            float RotX = ScreenToVP.x - StartRot.x;
            float RotY = ScreenToVP.y - StartRot.y;
            transform.position += new Vector3(DeltaX, DeltaY, 0); //movimenta o objeto de acordo com a movimentacao do dedo
            transform.Rotate(new Vector3 (-RotY*500, 0, RotX*50)); //faz o objeto rotacionar conforme se move, o objetivo é fazer a parte de baixo (pivot) rotacionar dar um efeito de inclinaçào na hora que move, como se estivesse chacoalhando.            
            StartPos = new Vector3(ScreenToWorld.x,ScreenToWorld.y, 0); //faz a leitura do toque do frame atual para fazer a comparacao com o proximo
            StartRot = new Vector3(ScreenToVP.x, ScreenToVP.y, 0);

        } 
        
    }
}
