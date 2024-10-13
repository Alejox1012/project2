open System

// Initialize the board as an array of 9 empty strings
let mutable board = Array.create 9 " "

// Display the current board in a 3x3 grid
let printBoard () =
    printfn "\n %s | %s | %s " board.[0] board.[1] board.[2]
    printfn "---|---|---"
    printfn " %s | %s | %s " board.[3] board.[4] board.[5]
    printfn "---|---|---"
    printfn " %s | %s | %s " board.[6] board.[7] board.[8]
    printfn ""

// Determine if a player has won by checking for winning lines
let checkWin player =
    // List of possible winning combinations (rows, columns, diagonals)
    let winningCombos = [
        [0; 1; 2]; [3; 4; 5]; [6; 7; 8] // rows
        [0; 3; 6]; [1; 4; 7]; [2; 5; 8] // columns
        [0; 4; 8]; [2; 4; 6]            // diagonals
    ]
    // Check if any winning combo is fully marked by the player
    winningCombos |> List.exists (fun combo -> combo |> List.forall (fun i -> board.[i] = player))

// Randomly choose the starting player
let mutable player = if Random().Next(2) = 0 then "X" else "O"
printfn "Starting player: %s" player

// Main game loop
let mutable gameOver = false
let mutable moves = 0

while not gameOver && moves < 9 do
    printBoard ()
    printf "Player %s, choose a position (1-9): " player
    let position = Console.ReadLine() |> int |> (fun x -> x - 1) // Get position and adjust for zero-index

    if position >= 0 && position < 9 && board.[position] = " " then
        // Mark the chosen position for the current player
        board.[position] <- player
        moves <- moves + 1

        // Check for a win or switch player
        if checkWin player then
            printBoard ()
            printfn "Player %s wins!" player
            gameOver <- true
        else
            player <- if player = "X" then "O" else "X"
    else
        printfn "Invalid move. Try again."

if not gameOver then
    printBoard ()
    printfn "It's a tie!"