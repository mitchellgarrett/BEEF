SRC_DIR = Source
SRC = $(wildcard $(SRC_DIR)/*.cs)
BUILD_DIR = Build
EXE = beef.exe

CSC_FLAGS = -errorendlocation

.PHONY: all
all: $(EXE)

$(EXE): $(SRC)
	@mkdir -p $(BUILD_DIR)
	@csc $(SRC) -out:$(BUILD_DIR)/$(EXE) $(CSC_FLAGS)

run: $(EXE)
	@mono $(BUILD_DIR)/$(EXE)

clean:
	@rm -r $(BUILD_DIR)
