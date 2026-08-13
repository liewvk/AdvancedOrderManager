Option Explicit On
Option Strict On
Option Infer On

Public Interface IInputValidator(Of In T)

    Function Validate(
        value As T) _
        As InputValidationResult

End Interface

