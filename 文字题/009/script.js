/**
 ','*','Created','by','wangjianxiong','on','2016/12/29.
 ','*/
//量词
var liangci = ['碟', '事', '册', '丘', '乘', '下', '丈', '丝', '两', '举', '具', '美', '包', '厘', '刀', '分', '列', '则', '剂', '副', '些', '匝', '队', '陌', '陔', '部', '出', '个', '介', '令', '份', '伙', '件', '任', '倍', '儋', '卖', '亩', '记', '双', '发', '叠', '节', '茎', '莛荮', '落', '蓬', '蔸', '巡', '过', '进', '通', '造', '遍', '道', '遭', '对', '尊', '头', '套', '弓', '引', '张', '弯', '开', '庄', '床', '座', '庹', '帖', '帧', '席', '常', '幅', '幢', '口', '句', '号', '台', '只', '吊', '合', '名', '吨', '和', '味', '响', '骑', '门', '间', '阕', '宗', '客', '家', '彪', '层', '尾', '届', '声', '扎', '打', '扣', '把', '抛', '批', '抔', '抱', '拨', '担', '拉', '抬', '拃', '挂', '挑', '挺', '捆', '掬', '排', '捧', '掐', '搭', '提', '握', '摊', '摞', '撇', '撮', '汪', '泓', '泡', '注', '浔', '派', '湾', '溜', '滩', '滴', '级', '纸', '线', '组', '绞', '统', '绺', '综', '缕', '缗', '场', '块', '坛', '垛', '堵', '堆', '堂', '塔', '墩', '回', '团', '围', '圈', '孔', '贴', '点', '煎', '熟', '车', '轮', '转', '载', '辆', '料', '卷', '截', '户', '房', '所', '扇', '炉', '炷', '觉', '斤', '笔', '本', '朵', '杆', '束', '条', '杯', '枚', '枝', '柄', '栋', '架', '根', '桄', '梃', '样', '株', '桩', '梭', '桶', '棵', '榀', '槽', '犋', '爿', '片', '版', '歇', '手', '拳', '段', '沓', '班', '文', '曲', '替', '股', '肩', '脬', '腔', '支', '步', '武', '瓣', '秒', '秩', '钟', '钱', '铢', '锊', '铺', '锤', '锭', '锱', '章', '盆', '盏', '盘', '眉', '眼', '石', '码', '砣', '碗', '磴', '票', '罗', '畈', '番', '窝', '联', '缶', '耦', '粒', '索', '累', '緉', '般', '艘', '竿', '筥', '筒', '筹', '管', '篇', '箱', '簇', '角', '重', '身', '躯', '酲', '起', '趟', '面', '首', '项', '领', '顶', '颗', '顷', '袭', '群', '袋'];
//名词
var mingci = ['乌柏树', '村民', '小孩', '老者', '青布长袍', '梨花木板', '小羯鼓', '四口', '金兵', '三角眼', '盘子', '小酒店', '拐杖', '黄酒', '蚕豆', '咸花生', '豆腐干', '咸蛋', '耳朵', '肢', '香蕉', '糖'];
var RESULT = [];
var test = '';
function getValue() {
    this.test = jQuery("#test").val();
    var sign = ['，', '。', '‘', '’', '“', '”', '：', '；', '\n'];
    // console.log(this.test);
    var strs = processString([this.test], sign, false);
    console.log(strs);
    process2(strs);
}
/**
 * 将内容根据符号划分成一个个数组
 * @param str
 * @param sign
 * @param end
 * @returns {any}
 */
function processString(str, sign, end) {
    if (end) {
        return str;
    }
    var result = [];
    for (var _i = 0, sign_1 = sign; _i < sign_1.length; _i++) {
        var s1 = sign_1[_i];
        result = [];
        for (var i = 0; i < str.length; i++) {
            result = result.concat(str[i].split(s1));
        }
        str = result;
    }
    return result;
}
function process2(test) {
    for (var _i = 0, test_1 = test; _i < test_1.length; _i++) {
        var str_1 = test_1[_i];
        for (var i = 0; i < str_1.length; i++) {
            i = process3(str_1, i);
        }
    }
    //最后结果
    console.log(this.RESULT);
    var str = '';
    for (var _a = 0, RESULT_1 = RESULT; _a < RESULT_1.length; _a++) {
        var r = RESULT_1[_a];
        jQuery('#result').append(r[0] + r[1] + r[2] + '<br>');
    }
}
function process3(str, index) {
    var theNum;
    //如果是数字
    if (isNum(str.charAt(index)) && index < str.length - 1) {
        theNum = str.charAt(index);
        index++;
        // 下一个如果还是数字
        if (isNum(str.charAt(index)) && index < str.length - 1) {
            //    数字是什么
            theNum = str.charAt(index - 1) + str.charAt(index);
            // console.log(theNum);
            //    再指向下一个
            index++;
        }
        var count = 0;
        for (var _i = 0, liangci_1 = liangci; _i < liangci_1.length; _i++) {
            var liang = liangci_1[_i];
            if (str.charAt(index) === liang) {
                break;
            }
            count++;
        }
        //相等，说明不是量词,可能是一个名词
        if (count === liangci.length) {
            index++;
            if (!isNum(str.charAt(index)) && index < str.length) {
                //验证是不是名词
                var loc = porcess4(str.slice(index - 1, str.length));
                if (loc[0] >= 0) {
                    //将结果存储
                    this.RESULT[this.RESULT.length] = ['', '', ''];
                    this.RESULT[this.RESULT.length - 1][0] = theNum;
                    //根据量词知道数量
                    this.RESULT[this.RESULT.length - 1][1] = '个';
                    this.RESULT[this.RESULT.length - 1][2] = loc[1];
                }
            }
        }
        else {
            //    不相等，说明是量词，那么下一个是不是个数字，不是可能是名词
            index++;
            if (!isNum(str.charAt(index))) {
                //验证是不是名词
                var loc = porcess4(str.slice(index - 1, str.length));
                if (loc[0] > 0) {
                    //将结果存储
                    this.RESULT[this.RESULT.length] = ['', '', ''];
                    this.RESULT[this.RESULT.length - 1][0] = theNum;
                    //根据量词知道数量
                    this.RESULT[this.RESULT.length - 1][1] = str.slice(index - 1, index);
                    this.RESULT[this.RESULT.length - 1][2] = loc[1];
                }
            }
        }
    }
    return index;
}
function porcess4(str) {
    var index = 0;
    for (var _i = 0, mingci_1 = mingci; _i < mingci_1.length; _i++) {
        var s = mingci_1[_i];
        //大于0,说明有
        index = str.indexOf(s);
        if (index >= 0) {
            return [index, s];
        }
    }
    return [index, ''];
}
/**
 * 是否为数字
 * @param c
 * @returns {boolean}
 */
function isNum(c) {
    if (c === "一") {
        // console.log(c);
        return true;
    }
    if (c === "二") {
        return true;
    }
    if (c === "三") {
        return true;
    }
    if (c === "四") {
        // console.log(c);
        return true;
    }
    if (c === "五") {
        return true;
    }
    if (c === "六") {
        return true;
    }
    if (c === "七") {
        return true;
    }
    if (c === "八") {
        return true;
    }
    if (c === "九") {
        return true;
    }
    if (c === "零") {
        return true;
    }
    if (c === '1') {
        // console.log(c);
        return true;
    }
    if (c === "2") {
        return true;
    }
    if (c === "3") {
        return true;
    }
    if (c === "4") {
        return true;
    }
    if (c === "5") {
        return true;
    }
    if (c === "6") {
        return true;
    }
    if (c === "7") {
        return true;
    }
    if (c === "8") {
        return true;
    }
    if (c === "9") {
        return true;
    }
    if (c === "0") {
        return true;
    }
    if (c === "壹") {
        return true;
    }
    if (c === "贰") {
        return true;
    }
    if (c === "叁") {
        return true;
    }
    if (c === "肆") {
        return true;
    }
    if (c === "伍") {
        return true;
    }
    if (c === "陆") {
        return true;
    }
    if (c === "柒") {
        return true;
    }
    if (c === "捌") {
        return true;
    }
    if (c === "玖") {
        return true;
    }
    if (c === "拾") {
        return true;
    }
    if (c === "两") {
        // console.log(c);
        return true;
    }
    return false;
}
