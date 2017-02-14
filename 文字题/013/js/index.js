$("button").bind('click', function () {
    $("div").empty();
    var num, name,j,i,k;
    var text = $("textarea").val();
    for (i = 0; i < text.length; i++) {
        num = 0;
        switch (text[i]) {
            case "一":
                if (text[i + 1] != "十") {
                    num += 1;
                }
                break;
            case "二": case "两":
                if (text[i + 1] != "十") {
                    num += 2;
                }
                break;
            case "三":
                if (text[i + 1] != "十") {
                    num += 3;
                }
                break;
            case "四":
                if (text[i + 1] != "十") {
                    num += 4;
                }
                break;
            case "五":
                if (text[i + 1] != "十") {
                    num += 5;
                }
                break;
            case "六":
                if (text[i + 1] != "十") {
                    num += 6;
                }
                break;
            case "七":
                if (text[i + 1] != "十") {
                    num += 7;
                }
                break;
            case "八":
                if (text[i + 1] != "十") {
                    num += 8;
                }
                break;
            case "九":
                if (text[i + 1] != "十") {
                    num += 9;
                }
                break;
            case "1": case "2": case "3": case "4": case "5": case "6": case "7": case "8": case "9": case "10":
            case "11": case "12": case "13": case "14": case "15": case "16": case "17": case "18": case "19": case "20":
                num = text[i];
                break;
            case "十":
                i++;
                switch (text[i - 2]) {
                    case "二":
                        num = 20;
                        break;
                    case "三": case "四": case "五": case "六": case "七": case "八": case "九":
                        continue;
                        break;
                    default:
                        num = 10;
                        break;
                }
                switch (text[i]) {
                    case "一":
                        num += 1;
                        break;
                    case "二":
                        num += 2;
                        break;
                    case "三":
                        num += 3;
                        break;
                    case "四":
                        num += 4;
                        break;
                    case "五":
                        num += 5;
                        break;
                    case "六":
                        num += 6;
                        break;
                    case "七":
                        num += 7;
                        break;
                    case "八":
                        num += 8;
                        break;
                    case "九":
                        num += 9;
                        break;
                    default:
                        i--;
                        break;
                }
                break;
            default:
                continue;
                break;
        }
        switch (text[i + 1]) {
            case "个": case "只": case "把": case "部": case "件": case "头": case "滴": case "朵": case "块": case "辆": case "本":
            case "条": case "枝": case "根": case "杆": case "片": case "支": case "顶": case "面": case "株": case "面": case "柄":
            case "名": case "间": case "张":
                break;
            case "双": case "对":
                num = num * 2;
                break;
            default:
                continue;
                break;
        }
        j = 0;
        name = text.slice(i + 2, i + 6);
        switch (name) {
            case "梨花木板":
                j = 1;
                break;
        }
        if (j == 0) {
            name = text.slice(i + 2, i + 5);
            switch (name) {
                case "乌柏树": case "大松树": case "小羯鼓": case "小酒店": case "大瓦罐": case "黑衣人":
                    j = 1;
                    break;
            }
        }
        if (j == 0) {
            name = text.slice(i + 2, i + 4);
            switch (name) {
                case "盘子": case "酒杯": case "单刀": case "金壶": case "金杯": case "热茶": case "匕首": case "兵丁": case "女子": case "毡毯":
                case "少年": case "包裹": case "耳朵": case "香蕉":
                    j = 1;
                    break;
            }
        }
        if (j == 0) {
            name = text.slice(i + 2, i + 3);
            switch (name) {
                case "人": case "房": case "糖":
                    j = 1;
                    break;
            }
        }
        if (j == 1) {
            for (k = 0; k < num; k++) {
                $("div").append('<img src="./img/' + name + '.png" />');
            }
        }
    }
});

