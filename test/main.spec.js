describe("Calculator", function(){

	var calculator;
	beforeEach(function(){
		calculator = new Calculator();
	});
	it("should be correctly initialized", function(){
		
	});
	it("should be correctly initialized", function(){
		var result = calculator.add(23,7);
		expect(result).toEqual(30);
	});

	it("should be correctly initialized", function(){
		var result = calculator.subtract(23,7);
		expect(result).toEqual(16);
	});

	it("should be correctly initialized", function(){
		var result = calculator.add(-10,-5);
		expect(result).toEqual(-15);
	});
	it("should be correctly initialized", function(){
		var result = calculator.multiply(9,2);
		expect(result).toEqual(18);
	});
	it("should be correctly initialized", function(){
		var result = calculator.multiply(-9,2);
		expect(result).toEqual(-18);
	});
	it("should be correctly initialized", function(){
		var result = calculator.multiply(-9,-2);
		expect(result).toEqual(18);
	});
	it("should be correctly initialized", function(){
		var result = calculator.divide(4,2);
		expect(result).toEqual(2);
	});
	it("should be correctly initialized", function(){
		var result = calculator.divide(2,4);
		expect(result).toEqual(0.5);
	});
	it("should be correctly initialized", function(){
		var result = calculator.divide(-6,3);
		expect(result).toEqual(-2);
	});
	it("should be correctly initialized", function(){
		var result = calculator.powerOf(9,2);
		expect(result).toEqual(81);
	});
	it("should be correctly initialized", function(){
		var result = calculator.root(9);
		expect(result).toEqual(3);
	});

});
