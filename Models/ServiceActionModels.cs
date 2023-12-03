namespace TravelAPI.Models
{
    // Résultat d'une action de service
    public class ServiceActionResult
    {
        // Indique le succès de l'action
        public bool IsSuccess { get; set; }

        // Si une erreur métier s'est produite, contient les détails de l'erreur
        public ServiceActionError? Error { get; set; }

        public ServiceActionResult()
        { }

        public ServiceActionResult(bool isSuccess, ServiceActionError? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        // Méthode utilitaire type "factory" pour générer rapidement un retour de type succès
        public static ServiceActionResult FromSuccess()
        {
            return new ServiceActionResult(true, null);
        }

        // Méthode utilitaire type "factory" pour générer rapidement un retour de type erreur
        public static ServiceActionResult FromError(string errorMessage, ServiceActionErrorReason reason)
        {
            ServiceActionError error = new(errorMessage, reason);
            return new ServiceActionResult(false, error);
        }
    }

    // Résultat d'une action de service avec un retour spécifique
    public class ServiceActionResult<T> : ServiceActionResult // Hérite du résultat sans retour
    {
        // Retour générique pouvant être null
        public T? Result { get; set; }

        public ServiceActionResult(bool isSuccess, ServiceActionError? error, T? result = default) : base(isSuccess, error)
        {
            Result = result;
        }

        public static ServiceActionResult<T> FromSuccess(T? result)
        {
            return new ServiceActionResult<T>(true, null, result);
        }

        public static ServiceActionResult<T> FromError(string errorMessage, ServiceActionErrorReason reason, T? result = default)
        {
            ServiceActionError error = new(errorMessage, reason);
            return new ServiceActionResult<T>(false, error, result);
        }
    }

    // Erreur d'une action de service (cas géré et attendu)
    public class ServiceActionError
    {
        // Message de l'erreur
        public string ErrorMessage { get; set; }

        // Cause de l'erreur, utile pour déterminer le statut HTTP
        public ServiceActionErrorReason Reason { get; set; }

        public ServiceActionError(string errorMessage, ServiceActionErrorReason reason)
        {
            ErrorMessage = errorMessage;
            Reason = reason;
        }
    }

    // Causes possibles d'une erreur d'action de service
    // La liste peut être augmentée au fil du développement
    public enum ServiceActionErrorReason
    {
        BusinessRule = 400,
        NotFound = 404,
        // ... (autres raisons possibles)
    }
}
