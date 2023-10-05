$(document).ready(() => {
    //Receiver
    var citis_1 = document.getElementById("city1");
    var districts_1 = document.getElementById("district1");
    var wards_1 = document.getElementById("ward1");
    var cty_1 = document.getElementById("cty1");
    var huyen_1 = document.getElementById("quan-huyen1");
    var phuong_1 = document.getElementById("xa-phuong1");
    //var idCity;
    var Parameter = {
        url: "https://raw.githubusercontent.com/kenzouno1/DiaGioiHanhChinhVN/master/data.json",
        method: "GET",
        responseType: "application/json"
    };
    var promise = axios(Parameter);
    promise.then(function (resutl) {        
        RenderCityReceiver(resutl.data);
    });

    //Receiver
    function RenderCityReceiver(data) {
        for (const x of data) {
            citis_1.options[citis_1.options.length] = new Option(x.Name, x.Id);
        };
        citis_1.onchange = function () {
            districts_1.length = 1;
            wards_1.length = 1;
            if (this.value != "") {
                const result = data.filter(n => n.Id === this.value);

                for (const k of result[0].Districts) {
                    districts_1.options[districts_1.options.length] = new Option(k.Name, k.Id);
                }
                var ctyName_1 = [];
                $.each($(".name-city1 option:selected"), function () {
                    ctyName_1.push(this.text);
                });
                cty_1.value = ctyName_1;
                $('#cty1').html(cty_1.value);
            }
        };
        districts_1.onchange = function () {
            wards_1.length = 1;
            const dataCity = data.filter((n) => n.Id === citis_1.value);
            if (this.value != "") {
                const dataWards = dataCity[0].Districts.filter(n => n.Id === this.value)[0].Wards;

                for (const w of dataWards) {
                    wards_1.options[wards_1.options.length] = new Option(w.Name, w.Id);
                }
                var dist = [];
                $.each($(".district1 option:selected"), function () {
                    dist.push(this.text);
                });
                huyen_1.value = dist;
                $('#quan-huyen1').html(huyen_1.value);
            }
        };
        wards_1.onchange = function () {
            if (this.value != "") {
                var xa = [];
                $.each($(".ward1 option:selected"), function () {
                    xa.push(this.text);
                });
                phuong_1.value = xa;
                $('#xa-phuong1').html(phuong_1.value);
            }
        }
    }
});

