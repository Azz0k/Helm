import {InputGroup, InputGroupAddon, InputGroupInput} from "@/components/ui/input-group.tsx";
import {SearchIcon} from "lucide-react";

export const SearchPanel = ()=>{
  return (
    <div>
      <div
        className="text-sm items-center justify-between text-muted-foreground bg-muted/5 px-4 py-2 rounded-md md:min-w-55 cursor-pointer hidden md:flex"
      >
        <InputGroup>
          <InputGroupInput placeholder="Search..." />
          <InputGroupAddon>
            <SearchIcon />
          </InputGroupAddon>
        </InputGroup>
      </div>
    </div>
  );
}