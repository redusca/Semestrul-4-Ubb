using model;
using networking.DTO;

namespace networking.jsonProtocol.responseAndrequest
{
    public class RRBuilder
    {
        //Requests
        public static Request CreateLoginRequest(User user)
        {
            return new Request
            {
                type = requestType.LOGIN,
                User = new UserDTO(user)
            };
        }

        public static Request CreateLogoutRequest(User user)
        {
            return new Request
            {
                type = requestType.LOGOUT,
                User = new UserDTO(user)
            };
        }

        public static Request CreateGetBugsRequest()
        {
            return new Request
            {
                type = requestType.GET_BUGS
            };
        }

        public static Request CreateAddBugsRequest(IEnumerable<Bug> bugs)
        {
            return new Request
            {
                type = requestType.ADD_BUGS,
                bugDTOs = bugs.Select(b => new BugDTO(b)).ToArray()
            };
        }

        public static Request CreateStatusBugsRequest(Bug bug)
        {
            return new Request
            {
                type = requestType.STATUS_BUG,
                bugDTOs = new[] { new BugDTO(bug) }
            };
        }

        public static Request CreateCreateUserRequest(User user)
        {
            return new Request
            {
                type = requestType.CREATE_USER,
                User = new UserDTO(user)
            };
        }
        //Responses

        public static Response CreateOkResponse()
        {
            return new Response
            {
                type = responseType.OK
            };
        }

        public static Response CreateErrorResponse(string errorMessage)
        {
            return new Response
            {
                type = responseType.ERROR,
                errorMessage = errorMessage
            };
        }

        public static Response CreateLoginResponse(User user)
        {
            return new Response
            {
                type = responseType.LOGGED_CLIENT,
                user = new UserDTO(user)
            };
        }

        public static Response CreateGetBugsResponse(IEnumerable<Bug> bugs)
        {
            return new Response
            {
                type = responseType.REQUESTED_BUGS,
                bugDTOs = bugs.Select(b => new BugDTO(b)).ToArray()
            };
        }

        public static Response CreateNewBugsResponse(IEnumerable<Bug> bugs)
        {
            return new Response
            {
                type = responseType.NEW_BUGS,
                bugDTOs = bugs.Select(b => new BugDTO(b)).ToArray()
            };
        }

        public static Response CreateStatusBugChangedResponse(Bug bug)
        {
            return new Response
            {
                type = responseType.STATUS_BUG_CHANGED,
                bugDTOs = new[] { new BugDTO(bug) }
            };
        }
    }
}
