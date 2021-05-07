using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class SelectionController : MonoBehaviour
{
    //enum que define qual é o tipo do objeto
    public enum SelectableObjects
    {
        pickUpObject,
        openObject,
        readObject,
        puzzleObject,
        interactableObject
    }

    public SelectableObjects selectableObjects;

    #region PickUp Object Variables

    //enum que define qual é o tipo de objeto de pick up
    public enum PickUpObjects
    {
        rightHand,
        leftHand,
        none
    }

    public PickUpObjects pickUpObjects;

    //enum que define qual é o tipo de objeto dá mão direit
    public enum RightHandObjects
    {
        screwdriver,
        hammer,
        pipeWrench,
        none
    }

    public RightHandObjects rightHandObjects;

    public Transform rightHandConteiner, leftHandConteiner;
    private Transform pickUpObjectTransform;
    public static Transform activeRightHandObject, activeLeftHandObject;

    public static bool swapLeftHandTool = true; 

    private SelectionController pickUpObjectSelectionController;

    public GameObject correspondingObject;

    #endregion

    #region Open Object Variables

    private Animator animator;

    #endregion

    #region Read Object Variables

    public Sprite sprite;

    public Image showUpImage;

    public GameObject showUpImageObject;

    #endregion

    #region Puzzle Object Variables

    //enum que define qual o tipo de ferramenta é necessária para ativar o puzzle
    public enum NeededTool
    {
        screwdriver,
        hammer,
        pipeWrench,
        none
    }

    public NeededTool neededTool;

    private PuzzleController puzzleController;

    public static string activeRightHandTool;

    #endregion

    #region Interactable Object Variables

    public UnityEvent eventToTrigger;

    #endregion

    private Outline outline;

    private OutlinePresets outlinePreset;

    public static Transform _selection;
    
    //serve para inicializar as variáveis dos diferentes tipos de objetos
    IEnumerator Start()
    {
        gameObject.tag = "Selectable";

        SelectableObjects obj = selectableObjects;

        switch (obj)
        {
            case SelectableObjects.pickUpObject:

                pickUpObjectTransform = GetComponent<Transform>();
                pickUpObjectSelectionController = GetComponent<SelectionController>();

                neededTool = NeededTool.none;

                if (!(pickUpObjects == PickUpObjects.rightHand))
                    rightHandObjects = RightHandObjects.none;

                SetupOutline();

                yield return new WaitForSeconds(0.1f);

                outline.enabled = false;
                
                break;

            case SelectableObjects.openObject:
                
                pickUpObjects = PickUpObjects.none;
                rightHandObjects = RightHandObjects.none;
                neededTool = NeededTool.none;

                animator = GetComponent<Animator>();

                SetupOutline();

                yield return new WaitForSeconds(0.1f);

                outline.enabled = false;
                
                break;

            case SelectableObjects.readObject:
                
                pickUpObjects = PickUpObjects.none;
                rightHandObjects = RightHandObjects.none;
                neededTool = NeededTool.none;

                SetupOutline();

                yield return new WaitForSeconds(0.1f);

                outline.enabled = false;
                
                break;

            case SelectableObjects.puzzleObject:
                
                puzzleController = GetComponent<PuzzleController>();

                pickUpObjects = PickUpObjects.none;
                rightHandObjects = RightHandObjects.none;

                SetupOutline();

                yield return new WaitForSeconds(0.1f);

                outline.enabled = false;

                break;

            case SelectableObjects.interactableObject:
                
                pickUpObjects = PickUpObjects.none;
                rightHandObjects = RightHandObjects.none;
                neededTool = NeededTool.none;

                SetupOutline();

                yield return new WaitForSeconds(0.1f);

                outline.enabled = false;

                break;
        }
    }

    //checa inputs e a colisão do ponteiro do mouse
    void Update()
    {
        if (Input.GetButtonDown("Click"))
        {
            CheckClick();
        }

        if (Input.GetButtonDown("DropTool") && pickUpObjects == PickUpObjects.rightHand)
        {
            if (activeRightHandObject != null)
            {
                rightHandConteiner.GetChild(0).GetComponent<SelectionController>().DropRightHandTool();
            }
        }

        if (Input.GetButtonDown("SwapTool") && pickUpObjects == PickUpObjects.leftHand)
        {
            if (leftHandConteiner.childCount >= 2)
                ChangeLeftHandTool();
        }

        CheckSelection();
    }

    //inicializa as outlines dos objetos
    void SetupOutline()
    {
        outlinePreset = Resources.Load<OutlinePresets>("OutlinePresets/Outline" + selectableObjects.ToString());
        outline = GetComponent<Outline>();
        outline.OutlineColor = outlinePreset.outlineColor;
        outline.OutlineWidth = outlinePreset.outlineWidth;
    }

    //realiza uma ação caso o jogador clique com o botão esquerdo do mouse em um objeto selecionado atráves da colisão entre o objeto e o ponteiro do mouse
    //ação difere entre os tipos de objeto
    void CheckClick()
    {
        SelectableObjects obj = selectableObjects;

        switch (obj)
        {
            //objetos coletáveis pelo jogador
            case SelectableObjects.pickUpObject:

                PickUpObjects pUO = pickUpObjects;

                switch (pUO)
                {
                    //na mão direita somente um objeto pode ser segurado por vez, por isso, caso o jogador pegue outro, o que estava na mão é solto
                    case PickUpObjects.rightHand:

                        if (_selection == gameObject.transform)
                        {
                            if (rightHandConteiner.childCount == 0)
                            {
                                PickUp(rightHandConteiner, ref activeRightHandObject, ref activeRightHandTool);
                            }
                            else
                            {
                                rightHandConteiner.GetChild(0).GetComponent<SelectionController>().DropRightHandTool();

                                PickUp(rightHandConteiner, ref activeRightHandObject, ref activeRightHandTool);
                            }
                        }

                        break;

                    //na mão esquerda o jogador pode coletar vários itens, e trocar entre eles
                    case PickUpObjects.leftHand:

                        if (_selection == gameObject.transform)
                        {
                            if (leftHandConteiner.childCount == 0)
                            {
                                PickUp(leftHandConteiner, ref activeLeftHandObject);
                            }
                            else
                            {
                                leftHandConteiner.GetChild(0).gameObject.GetComponent<SelectionController>().correspondingObject.SetActive(false);

                                PickUp(leftHandConteiner, ref activeLeftHandObject);
                            }
                        }

                        break;
                }

                break;

            //objetos que abrem e fecham
            case SelectableObjects.openObject:
                
                if (_selection == gameObject.transform)
                {
                    if (!animator.GetBool("IsOpen"))
                    {
                        animator.Play("DoorOpen");

                        animator.SetBool("IsOpen", true);

                        _selection = null;
                    }
                    else
                    {
                        animator.Play("DoorClose");
                       
                        animator.SetBool("IsOpen", false);

                        _selection = null;
                    }   
                }   

                break;

            //objetos como fotos e cartas, ao clickar, eles ficam em foco na tela
            case SelectableObjects.readObject:

                if (_selection == gameObject.transform)
                {
                    ShowUpImage();
                }

                break;

            //objetos que compõe puzzles, eles requerem diferentes ferramentas da mão direita para serem ativados, ou não requerem ferramentas
            case SelectableObjects.puzzleObject:

                if ((activeRightHandTool == neededTool.ToString() || neededTool == NeededTool.none) && _selection == gameObject.transform)
                {
                    puzzleController.ActivePuzzle();

                    _selection = null;
                }

                break;

            //objetos interativos que somente triggam eventos, diferente dos puzzles que podem triggar mais coisas de acorod com a Class PuzzleController
            case SelectableObjects.interactableObject:
                
                if (_selection == gameObject.transform)
                {
                    eventToTrigger.Invoke();

                    _selection = null;
                }
                
                break;
        }
    }

    //realiza o pick up do objeto
    //construtor destinado à mão esquerda
    void PickUp(Transform conteiner, ref Transform activeObject)
    {
        pickUpObjectTransform.position = conteiner.position;
        pickUpObjectTransform.SetParent(conteiner);

        if (pickUpObjectTransform.gameObject.GetComponent<Collider>() != null)
            pickUpObjectTransform.gameObject.GetComponent<Collider>().enabled = false;

        if (pickUpObjectTransform.gameObject.GetComponent<MeshRenderer>() != null)
            pickUpObjectTransform.gameObject.GetComponent<MeshRenderer>().enabled = false;

        if (pickUpObjectTransform.gameObject.GetComponent<Rigidbody>() != null)
            pickUpObjectTransform.gameObject.GetComponent<Rigidbody>().useGravity = false;

        if (pickUpObjectTransform.gameObject.transform.childCount > 0)
        {
            for (int i = 0; i < pickUpObjectTransform.gameObject.transform.childCount; i++)
            {
                pickUpObjectTransform.gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        pickUpObjectSelectionController.correspondingObject.transform.SetAsFirstSibling();
        pickUpObjectSelectionController.correspondingObject.SetActive(true);

        outline.enabled = false;
        activeObject = gameObject.transform;
        _selection = null;
    }

    //realiza o pick up do objeto
    //construtor destinado à mão direita
    void PickUp(Transform conteiner, ref Transform activeObject, ref string activeTool)
    {
        pickUpObjectTransform.position = conteiner.position;
        pickUpObjectTransform.SetParent(conteiner);

        if (pickUpObjectTransform.gameObject.GetComponent<Collider>() != null)
            pickUpObjectTransform.gameObject.GetComponent<Collider>().enabled = false;

        if (pickUpObjectTransform.gameObject.GetComponent<MeshRenderer>() != null)
            pickUpObjectTransform.gameObject.GetComponent<MeshRenderer>().enabled = false;

        if (pickUpObjectTransform.gameObject.GetComponent<Rigidbody>() != null)
            pickUpObjectTransform.gameObject.GetComponent<Rigidbody>().useGravity = false;

        if (pickUpObjectTransform.gameObject.transform.childCount > 0)
        {
            for (int i = 0; i < pickUpObjectTransform.gameObject.transform.childCount; i++)
            {
                pickUpObjectTransform.gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        pickUpObjectSelectionController.correspondingObject.transform.SetAsFirstSibling();
        pickUpObjectSelectionController.correspondingObject.SetActive(true);

        activeTool = rightHandObjects.ToString();

        outline.enabled = false;
        activeObject = gameObject.transform;
        _selection = null;
    }

    //solta o objeto presente na mão direita
    void DropRightHandTool()
    {
        Transform dropedTool = rightHandConteiner.GetChild(0);
        dropedTool.SetParent(null);

        if (pickUpObjectTransform.gameObject.GetComponent<Collider>() != null)
            pickUpObjectTransform.gameObject.GetComponent<Collider>().enabled = true;

        if (pickUpObjectTransform.gameObject.GetComponent<MeshRenderer>() != null)
            pickUpObjectTransform.gameObject.GetComponent<MeshRenderer>().enabled = true;

        if (pickUpObjectTransform.gameObject.GetComponent<Rigidbody>() != null)
            pickUpObjectTransform.gameObject.GetComponent<Rigidbody>().useGravity = true;

        if (pickUpObjectTransform.gameObject.transform.childCount > 0)
        {
            for (int i = 0; i < pickUpObjectTransform.gameObject.transform.childCount; i++)
            {
                pickUpObjectTransform.gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        pickUpObjectSelectionController.correspondingObject.SetActive(false);
        pickUpObjectSelectionController.correspondingObject.transform.SetAsLastSibling();

        activeRightHandObject = null;
        activeRightHandTool = "";
    }

    //altera o objeto ativo na mão esquerda
    void ChangeLeftHandTool()
    {
        if (swapLeftHandTool)
        {
            leftHandConteiner.GetChild(0).gameObject.GetComponent<SelectionController>().correspondingObject.SetActive(false);
            
            leftHandConteiner.GetChild(1).gameObject.GetComponent<SelectionController>().correspondingObject.SetActive(true);
            activeLeftHandObject = leftHandConteiner.GetChild(1).gameObject.transform;

            leftHandConteiner.GetChild(0).SetAsLastSibling();
            leftHandConteiner.GetChild(0).gameObject.GetComponent<SelectionController>().correspondingObject.transform.SetAsLastSibling();

            swapLeftHandTool = false;
        }
        else
        {
            swapLeftHandTool = true;
        }
    }

    //coloca a imagem do read object em foco no canvas
    void ShowUpImage()
    {
        _selection = null;

        showUpImageObject.SetActive(true);
        showUpImage.sprite = sprite;
        
        Cursor.lockState = CursorLockMode.None;
    }

    //checa colisão do ponteiro do mouse com um objeto
    void CheckSelection()
    {
        if (_selection != null)
        {
            var selectionOutline = _selection.GetComponent<Outline>();
            selectionOutline.enabled = false;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, outlinePreset.maxDistance))
        {
            var selection = hit.transform;

            if (selection.CompareTag("Selectable"))
            {
                if (outline != null)
                {
                    var selectionOutline = selection.GetComponent<Outline>();

                    selectionOutline.enabled = true;
                }

                _selection = selection;
            }
        }
    }
}
