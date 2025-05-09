namespace networking.jsonProtocol.responseAndrequest
{
    public enum requestType
    {
        LOGIN,
        LOGOUT,
        GET_BUGS,
        ADD_BUGS,
        STATUS_BUG,
        CREATE_USER
    }

    public enum responseType
    {
        OK,
        ERROR,
        LOGGED_CLIENT,
        REQUESTED_BUGS,
        //ObserverResponses
        NEW_BUGS,
        STATUS_BUG_CHANGED,
    }
}
