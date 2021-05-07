using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SelectionController))]
public class PuzzleController : MonoBehaviour
{
    public enum Puzzles
    {
        screwdriver,
        hammer,
        pipeWrench,
        none
    }

    public Puzzles puzzles;

    public UnityEvent eventToTrigger;

    //ativa os puzzles, filtrando através de um enum
    public void ActivePuzzle()
    {
        Puzzles puz = puzzles;

        switch (puz)
        {
            case Puzzles.screwdriver:

                eventToTrigger.Invoke();

                break;

            case Puzzles.hammer:

                eventToTrigger.Invoke();

                break;

            case Puzzles.pipeWrench:

                eventToTrigger.Invoke();

                break;

            case Puzzles.none:

                eventToTrigger.Invoke();

                break;
        }
    }
}
