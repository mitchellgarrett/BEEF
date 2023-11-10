SRC_DIR = src
SRC = $(wildcard $(SRC_DIR)/*.cs)
BUILD_DIR = build
EXE = beef.exe

ASM_DIR = asm
ASM_EXE = bisc-asm.exe
ASM_SRC = $(wildcard $(ASM_DIR)/*.cs) $(CMN_SRC)

VM_DIR = vm
VM_EXE = bisc-vm.exe
VM_SRC = $(wildcard $(VM_DIR)/*.cs) $(CMN_SRC) $(filter-out $(ASM_DIR)/Application.cs, $(wildcard $(ASM_DIR)/*.cs))

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
