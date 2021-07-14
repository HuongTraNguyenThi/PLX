
using System.Collections.Generic;

namespace PLX.API.Data.DTO.Customer
{
    public class GetQuestionResponse : ResultMessageResponse
    {
        public GetQuestionResponse(List<ListItem> listQuestions)
        {
            Questions = listQuestions;
        }
        public List<ListItem> Questions { get; set; }
    }
}
