$(document).ready(function() {
    $("#ok").click(function() {
        var da = $("#data").val();
        splitLongWords(da);
    })
});

function splitLongWords(longwords) {
    //  首先将段落按照标点符号分成数组
    var arr = longwords.split(/[，。！、]/);
    console.log(arr);
    // debugger;

    // 遍历处理数组中的每个短句
    for (var index = 0; index < arr.length-1; index++) {

        var resu = splitWords(arr[index]); //每个短句的分词结果
        // console.log(resu);

        var count = 1;
        var obj;

        //  在分词的数组结果中循环，寻找分词中的量词和需要的对象
        for (var ind = 0; ind < resu.length; ind++) {
            //  判断是否存在量词
            if (resu[ind] in countn) {
                count = countn[resu[ind]];
            }
            // 获取需要的对象
            obj = fn_GetObj(resu[ind]);

            // 若存在我们需要的对象
            if (obj != undefined) {
                console.log(count);
                console.log(obj);
                for (var k = 0; k < count; k++) {
                    $("#imagebox").append("<img src='" + obj.img + "'/>");
                }
            }
        }
    };

}

//  根据分词判断是否是需要的对象
function fn_GetObj(data) {
    for (var y = 0; y < obj.length; y++) {
        if (obj[y].name === data) {
            return obj[y];
        }
    }
}


function splitWords(words) {
    var start = 0,
        end = words.length - 1,
        result = [];
    while (start != end) {
        var str = [];
        for (var i = start; i <= end; i++) {
            var s = words.substring(i, i + 1);
            // 如果是停止词，则跳过
            if (s in stop) {
                break;
            }
            str.push(s);
            // 如果在字典中，则添加到分词结果集
            if (str.join('') in dict) {
                result.push(str.join(''));
            }
        }

        start++;
    }

    return result;
}