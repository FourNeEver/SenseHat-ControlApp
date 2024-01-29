from flask import Flask, request, jsonify
from sense_emu import SenseHat
# from sense_hat import SenseEmu
import json
import numpy as np

sense = SenseHat()
server = Flask(__name__)

middle_button_counter=0
x_axis_counter=0
y_axis_counter=0

class Joystick:
    c_middle_button_counter=0
    c_x_axis_counter=0
    c_y_axis_counter=0
    

@server.route('/getPixels', methods=['GET'])
def getPixels():
    return json.dumps(sense.get_pixels(), indent=2)


@server.route('/getOrientationData', methods=['GET'])
def getOrientationData():
    return json.dumps(sense.get_orientation_degrees(), indent=2)


@server.route('/getEnvironmentData', methods=['GET'])
def getEnvironmentData():
    h = sense.get_humidity()
    p = sense.get_pressure()
    t = sense.get_temperature()
    return json.dumps({'temperature': t, 'pressure': p, 'humidity': h}, indent=2)


@server.route('/getHumidity', methods=['GET'])
def getHumidity():
    h = sense.get_humidity()
    return json.dumps({'humidity': h}, indent=2)


@server.route('/getTemperature', methods=['GET'])
def getTemperature():
    t = sense.get_temperature()
    return json.dumps({'temperature': t}, indent=2)


@server.route('/getPressure', methods=['GET'])
def getPressure():
    p = sense.get_pressure()
    return json.dumps({'pressure': p}, indent=2)



@server.route('/setOnePixel', methods=['POST'])
def setSinglePixel():
    content = request.get_json(silent=True)
    sense.set_pixel(content["x"], content["y"], content["R"], content["G"], content["B"])
    return "Pixel Set"



@server.route('/setPixels', methods=['POST'])
def setPixels():
    content = request.get_json(silent=True)
    xtable = content["x"]
    ytable = content["y"]
    ly = len(ytable)
    lx = len(xtable)
    if lx == ly:
        for i in range(lx):
            sense.set_pixel(int(content["x"][i:i + 1]), int(content["y"][i:i + 1]), content["R"], content["G"],
                            content["B"])
        return "Pixels Set"
    else:
        return "Number of x and y values is not equal"


@server.route('/setPixels2', methods=['POST'])
def setPixels2():
    content = request.get_json(silent=True)
    # content = [[1,1,75,144,205],[1,3,75,144,205],[2,2,75,144,205],[2,6,53,116,167],[2,7,53,116,167],[3,5,75,40,56],[5,4,75,40,56],[6,5,75,40,56]]
    for i in content:
        print(i[0], " ", i[1], " ", i[2], " ", i[3], " ", i[4])
    # sense.set_pixel(i[0],i[1], i[2], i[3], i[4])
    return "Pixels Set"


@server.route('/setPixels3', methods=['POST'])
def setPixels3():
    contentbaza = request.form
    for x in range(8):
        for y in range(8):
            z = contentbaza.getlist('LED'+str(x) + str(y))
            if z:
                z = str(z)
                z = z[2:]
                z2 = z[:-2]
                string = z2
                arr = [int(i.strip()) for i in string[1:-1].split(",")]
                a = np.array(arr)
                sense.set_pixel(a[0], a[1], a[2], a[3], a[4])

    return "Pixels Set"



@server.route('/setScreen', methods=['POST'])
def setScreen():
    content = request.get_json(silent=True)
    c = (content["R"], content["G"], content["B"])
    pixels = [
        c, c, c, c, c, c, c, c,
        c, c, c, c, c, c, c, c,
        c, c, c, c, c, c, c, c,
        c, c, c, c, c, c, c, c,
        c, c, c, c, c, c, c, c,
        c, c, c, c, c, c, c, c,
        c, c, c, c, c, c, c, c,
        c, c, c, c, c, c, c, c
    ]
    sense.set_pixels(pixels)
    return "Screen set"


@server.route('/clearScreen', methods=['POST'])
def clearScreen():
    sense.clear()
    return "Screen Cleared"


@server.route('/getAllData', methods=['GET'])
def getAllData():
    h = sense.get_humidity()
    p = sense.get_pressure()
    t = sense.get_temperature()
    o = sense.get_orientation_degrees()
    #sense.stick.wait_for_event()
    #j = sense.stick.get_events()
    return json.dumps({'temperatura': t, 'pressure': p, 'humidity': h, 'orient': o}, indent=2)


@server.route('/Tabela', methods=['GET'])
def Tabela():

    middle_button_counter_arg=Joystick.c_middle_button_counter
    x_axis_counter_arg=Joystick.c_x_axis_counter
    y_axis_counter_arg=Joystick.c_y_axis_counter
    j_x={"name":"joystick_x","value":x_axis_counter_arg,"unit":"-"}
    j_y={"name":"joystick_y","value":y_axis_counter_arg,"unit":"-"}
    j_m={"name":"joystick_mid","value":middle_button_counter_arg,"unit":"-"}
    
    while True:
        for event in sense.stick.get_events():
            if event[2]=='pressed':
                if event[1]=='up':
                    y_axis_counter_arg+=1
                    Joystick.c_y_axis_counter+=1
                if event[1]=='down':
                    y_axis_counter_arg-=1
                    Joystick.c_y_axis_counter-=1
                if event[1]=='right':
                    x_axis_counter_arg+=1
                    Joystick.c_x_axis_counter+=1
                if event[1]=='left':
                    x_axis_counter_arg-=1
                    Joystick.c_x_axis_counter-=1
                if event[1]=='middle':
                    middle_button_counter_arg+=1
                    Joystick.c_middle_button_counter+=1

                    
                j_x={"name":"joystick_x","value":Joystick.c_x_axis_counter,"unit":"-"}
                j_y={"name":"joystick_y","value":Joystick.c_y_axis_counter,"unit":"-"}
                j_m={"name":"joystick_mid","value":Joystick.c_middle_button_counter,"unit":"-"}                
                result=json.dumps(j_x, j_y, j_m)
                print(result)
        h = sense.get_humidity()
        p = sense.get_pressure()
        t = sense.get_temperature()
        o = sense.get_orientation_degrees()
    #print(json.dumps({"joystick":j}))
        return json.dumps([{"name":"temperature","value":t,"unit":"C"},{"name":"pressure","value":p,"unit":"hPa"},{"name":"humidity","value":h,"unit":"%"},{"name":"roll","value":o["roll"],"unit":"deg"},{"name":"pitch ","value":o["pitch"],"unit":"deg"},{"name":"yaw ","value":o["yaw"],"unit":"deg"},j_x, j_y, j_m])




if __name__ == "__main__":
    server.run(port=8080, host="192.168.56.102")

