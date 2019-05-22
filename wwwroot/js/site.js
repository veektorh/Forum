// Write your JavaScript code.
$(".btnPostUpvote").click(function(){
    var postId = $(this).attr("data-id");
    $.get("/post/UpVotePost/"+postId,(res)=>{

        if(res.status){
            $(".postUpvote-"+postId).html(res.count);
            return;
        }
        alert('an error occured');
    });
    
});

$(".btnPostDownvote").click(function(){
    var postId = $(this).attr("data-id");
    $.get("/post/DownVotePost/"+postId,(res)=>{
        if(res.status){
            $(".postUpvote-"+postId).html(res.count);
            return;
        }
        alert('an error occured');
    });
    
});


$(document).on('click', '.btnCommentUpvote', function (e) {
    e.preventDefault();
    var commentId = $(this).attr("data-id");
    $.get("/post/UpVoteComment/"+commentId,(res)=>{

        if(res.status){
            $(".commentUpvote-"+commentId).html(res.count);
            return;
        }
        alert('an error occured');
    });
});

$(document).on('click', '.btnCommentDownvote', function (e) {
    e.preventDefault();
    var commentId = $(this).attr("data-id");
    $.get("/post/DownVoteComment/"+commentId,(res)=>{

        if(res.status){
            $(".commentUpvote-"+commentId).html(res.count);
            return;
        }

        alert('an error occured');
    });
});
