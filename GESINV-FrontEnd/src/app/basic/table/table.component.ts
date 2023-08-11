import { SelectionModel } from '@angular/cdk/collections';
import { AfterViewInit, Component, ContentChildren, ElementRef, EventEmitter, Input, OnInit, Output, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { InputComponent } from '../input-basic/input.component';
import { MatPaginator } from '@angular/material/paginator';



export interface ColumnasDef{
  columnDef: string;
  header: string;
  cell: (element:any)=>string;
}

export interface InputColumnOptions{
  nombre : string;
  tipo : string;
  placeholder : string;
  disabled : boolean;
  defaultval : string;
}

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit, AfterViewInit {

  @Input() datos!:any[];

  @Input()
  definicion_columnas!:ColumnasDef[];

  @Input()
  admite_seleccion:boolean = false;

  @Input()
  admite_remover:boolean = false;

  @Input()
  admite_Input:boolean = false;

  @Input()
  admite_paginacion:boolean = false;

  @Input()
  input_column_tittle:string = "Input";

  @Input()
  allow_example_vals_if_empty:boolean = true;

  @Input()
  input_column_options:InputColumnOptions = {
    nombre : "Input",
    tipo : "texto",
    placeholder : "",
    disabled : false,
    defaultval : "",
  };

  //dataSource!:any[];
  dataSource = new MatTableDataSource<any>();
  displayedColumns!:string[];
  columns!:any[];

  inputValue:string = "";

  @Output("elementoSeleccionado")
  elementoSeleccionado = new EventEmitter<any>();

  @Output("elementoParaRemover")
  elementoParaRemover = new EventEmitter<any>();

  @ViewChild(MatTable) 
  table!: MatTable<any>;

  @ViewChildren(InputComponent)
  inputComponents!: QueryList<InputComponent>;

  myInputText : any = [];

  constructor(){  }

  ngOnInit(): void {
    if(this.allow_example_vals_if_empty){
      this.dataSource.data = ELEMENT_DATA;
      this.columns = this.example_columns;
    }

    if(this.datos){
      this.dataSource.data = this.datos;
    }

    if(this.definicion_columnas){
      this.columns = this.definicion_columnas;
    }

    if(this.admite_Input){
      this.columns = this.columns.concat(this.GetInputColumnDef());
    }

    if(this.admite_seleccion){
      this.columns = this.columns.concat(this.COLUMNA_SELECCIONAR);
    }

    if(this.admite_remover){
      this.columns = this.columns.concat(this.COLUMNA_REMOVER);
    }

    this.displayedColumns = this.columns.map(c => c.columnDef);

    //console.log(Object.keys(ELEMENT_DATA[0]));
    //console.log(this.columns[0]);
  }

  @ViewChild("paginator") paginator!: MatPaginator;

  ngAfterViewInit() {
    if(this.admite_paginacion){
      this.dataSource.paginator = this.paginator;
    }
  }

  AgregarElemento(nuevoDato:any){
    if(this.dataSource.data.indexOf(nuevoDato) < 0){
      this.dataSource.data = this.dataSource.data.concat(nuevoDato);
      this.table.renderRows();
    }    
  }

  ActualizarFilas(nuevosDatos:any[]){
    this.dataSource.data = nuevosDatos;
    this.table.renderRows();
  }

  EmitirSeleccion(element:any){
    this.elementoSeleccionado.emit(element);
  }

  EmitirRemover(element:any){

    this.dataSource.data = this.dataSource.data.filter(e => e !== element);
    this.table.renderRows();
    this.elementoParaRemover.emit(element);
  }

  ObtenerCantidadDeFilas():number{
    return this.dataSource.data.length;
  }


  InputColumn_TodosLosValoresSonValidos():boolean{
    let todosValidos = true;

    this.inputComponents.forEach( input => 
        todosValidos = todosValidos && input.HasValidValueOrIsDisabled()
      );

    return todosValidos;
  }

  InputColumn_ObtenerTodosLosValoresComoNumber():number[]{
    let numeros:number[] = [];

    this.inputComponents.forEach( input => 
      numeros = numeros.concat(input.GetInputValueAsNumber())
    );

    return numeros;
  }

  InputColumn_ObtenerTodosLosValoresComoNumberYDatos():{item: any,numero: number}[]{
    let valores:{item: any,numero: number}[] = [];
    let inputs:InputComponent[] = this.inputComponents.toArray();

    for (let i = 0; i < this.dataSource.data.length; i++) {
      valores = valores.concat({
        item : this.dataSource.data[i],
        numero : inputs[i].GetInputValueAsNumber()
      })
    };

    return valores;
  }

  InputColumn_TocarTodosLosInput(){
    this.inputComponents.forEach( input => 
      input.MarkAsTouched()
    );
  }

  GetInputColumnValue(){
    return this.inputValue;
  }

  private GetInputColumnDef():any{
    return {
      columnDef: 'input',
      header: this.input_column_tittle,
      cell: (element: any) => 'Select',
      soyInput: true
    }
  }

  COLUMNA_SELECCIONAR = {
    columnDef: 'buttonSeleccionar',
    header: 'Seleccionar',
    cell: (element: any) => 'Select',
    soyButton: true
  }

  COLUMNA_REMOVER = {
    columnDef: 'buttonRemover',
    header: 'Remover',
    cell: (element: any) => 'Remover',
    soyButtonDelete: true
  }

//<mat-icon>delete</mat-icon>

example_columns = [
    {
      columnDef: 'position',
      header: 'No.',
      cell: (element: PeriodicElement) => `${element.position}`,
    },
    {
      columnDef: 'name',
      header: 'Name',
      cell: (element: PeriodicElement) => `${element.name}`,
    },
    {
      columnDef: 'weight',
      header: 'Weight',
      cell: (element: PeriodicElement) => `${element.weight}`,
    },
    {
      columnDef: 'symbol',
      header: 'Symbol',
      cell: (element: PeriodicElement) => `${element.symbol}`,
    },
  ];
}




export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'},
  {position: 2, name: 'Helium', weight: 4.0026, symbol: 'He'},
  {position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li'},
  {position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be'},
  {position: 5, name: 'Boron', weight: 10.811, symbol: 'B'},
  {position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C'},
  {position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N'},
  {position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O'},
  {position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F'},
  {position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne'},
];

