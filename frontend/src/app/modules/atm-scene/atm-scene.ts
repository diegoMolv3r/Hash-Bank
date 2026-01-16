import { Component, ElementRef, OnInit, ViewChild, AfterViewInit, OnDestroy, NgZone } from '@angular/core';
import * as THREE from 'three';
import { GLTFLoader } from 'three/examples/jsm/loaders/GLTFLoader';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls';
import { RoomEnvironment } from 'three/examples/jsm/environments/RoomEnvironment.js';
import gsap from 'gsap';

@Component({
  selector: 'app-atm-scene',
  templateUrl: './atm-scene.html',
  styleUrls: ['./atm-scene.css']
})
export class AtmSceneComponent implements AfterViewInit, OnDestroy {
  @ViewChild('rendererCanvas') rendererCanvas!: ElementRef<HTMLCanvasElement>;

  private renderer!: THREE.WebGLRenderer;
  private scene!: THREE.Scene;
  private camera!: THREE.PerspectiveCamera;
  private controls!: any;
  private animationId!: number;
  private raycaster = new THREE.Raycaster();
  private mouse = new THREE.Vector2();
  public isInterfaceActive = false;

  constructor(private ngZone: NgZone) { }

  private onMouseClick(event: MouseEvent): void {
    if (this.isInterfaceActive) return;

    const canvas = this.rendererCanvas.nativeElement;
    const rect = canvas.getBoundingClientRect();

    this.mouse.x = ((event.clientX - rect.left) / rect.width) * 2 - 1;
    this.mouse.y = -((event.clientY - rect.top) / rect.height) * 2 + 1;

    this.raycaster.setFromCamera(this.mouse, this.camera);
    const intersects = this.raycaster.intersectObjects(this.scene.children, true);

    if (intersects.length > 0) {
      console.log('Click detectado en el cajero');
      this.focusOnScreen();
    }
  }

  private focusOnScreen(): void {
    const targetPoint = new THREE.Vector3(0, 13, -12); 

    const cameraPoint = new THREE.Vector3(0, 21, 13); 

    this.controls.enabled = false;

    gsap.to(this.camera.position, {
      x: cameraPoint.x,
      y: cameraPoint.y,
      z: cameraPoint.z,
      duration: 1.5, 
      ease: 'power2.inOut'
    });

    gsap.to(this.controls.target, {
      x: targetPoint.x,
      y: targetPoint.y,
      z: targetPoint.z,
      duration: 1.5,
      ease: 'power2.inOut',
      onUpdate: () => this.controls.update(),
      onComplete: () => {
        this.isInterfaceActive = true;
      }
    });

    onComplete: () => {
      console.log("Animación terminada. Activando interfaz..."); 

      this.ngZone.run(() => {
        this.isInterfaceActive = true;
      });
    } 
  }

  public exitAtm(): void {
    this.isInterfaceActive = false;
    this.controls.enabled = true; 

    gsap.to(this.camera.position, {
      x: 0, 
      y: 1.5, 
      z: 5,
      duration: 1.2,
      ease: 'power2.inOut'
    });

    gsap.to(this.controls.target, {
      x: 0, y: 0, z: 0,
      duration: 1.2,
      onUpdate: () => this.controls.update()
    });
  }
  
  ngAfterViewInit(): void {
    this.initThree();
    this.loadModel();
    this.animate();
    
    // Ajustar si el usuario cambia el tamaño de la ventana
    this.rendererCanvas.nativeElement.addEventListener('click', (event) => this.onMouseClick(event));
    window.addEventListener('resize', () => this.onWindowResize());
  }
  
  private initThree(): void {
    const canvas = this.rendererCanvas.nativeElement;
    const width = canvas.parentElement?.clientWidth || window.innerWidth;
    const height = canvas.parentElement?.clientHeight || window.innerHeight;

    this.scene = new THREE.Scene();
    this.scene.background = new THREE.Color(0x1a1a2e); // Color azul oscuro estilo "banco seguro"

    // 2. CÁMARA
    // poner la perspectiva inicial en el centro 
    this.camera = new THREE.PerspectiveCamera(45, width / height, 0.1, 1000);
    this.camera.position.set(-0.33, 21.41, 34.43); // Posición inicial x y z
   

    // 3. RENDERER
    this.renderer = new THREE.WebGLRenderer({ canvas, antialias: true, alpha: true });
    this.renderer.setSize(width, height);
    this.renderer.setPixelRatio(window.devicePixelRatio);
    this.renderer.shadowMap.enabled = true; // Activar sombras

    this.renderer.outputColorSpace = THREE.SRGBColorSpace;
    this.renderer.toneMapping = THREE.ACESFilmicToneMapping;
    this.renderer.toneMappingExposure = 0.5;

    // 4. LUCES
    const ambientLight = new THREE.AmbientLight(0xffffff, 0.6);
    this.scene.add(ambientLight);

    // Bajar la luz direccional (el sol)
    const directionalLight = new THREE.DirectionalLight(0xffffff, 0.5); // <--- Estaba en 1
    directionalLight.position.set(5, 10, 7);
    directionalLight.castShadow = true;
    this.scene.add(directionalLight);
    const pmremGenerator = new THREE.PMREMGenerator(this.renderer);

    pmremGenerator.compileEquirectangularShader();
    this.scene.environment = pmremGenerator.fromScene(new RoomEnvironment(), 0.04).texture;


    // 5. CONTROLES (Para rotar con el mouse mientras desarrollas)
    this.controls = new OrbitControls(this.camera, this.renderer.domElement);
    this.controls.enableDamping = true; // Movimiento suave

    this.controls.target.set(-1.86, 10.51, -2.79);

    this.controls.update();

    // INICIAL
    // Cámara (Position): x: -0.33, y: 21.41, z: 34.43
    // Mira a (Target):   x: -1.86, y: 10.51, z: -2.79

    // ZOOM AL CAJERO
    // Cámara (Position): x: -1.34, y: 20.73, z: 12.95
    // Mira a (Target):   x: -1.10, y: 15.62, z: -4.50

    // ZOOM A LA PANTALLA
    // Cámara (Position): x: -1.48, y: 19.68, z: 6.02
    // Mira a (Target):   x: -1.70, y: 16.32, z: -4.34
  }

  private loadModel(): void {
    const loader = new GLTFLoader();
    
    // Ruta del modelo GLB
    loader.load('/assets/models/cajero.glb', (gltf: any) => {
      const model = gltf.scene;
      
      // Ajustes comunes al modelo
      model.position.set(0, 0, 0); 
      model.scale.set(1, 1, 1); // Juega con esto si el modelo es muy grande o chico
      
      // Activar sombras en el modelo
      model.traverse((child: any) => {
        if (child.isMesh) {
          child.castShadow = true;
          child.receiveShadow = true;
        }
      });

      this.scene.add(model);
      console.log('Modelo cargado exitosamente');

    }, undefined, (error: any) => {
      console.error('Error cargando el modelo:', error);
    });
  }

  private animate(): void {
    // Loop de renderizado (se ejecuta 60 veces por segundo)
    this.animationId = requestAnimationFrame(() => this.animate());
    
    if (this.controls) this.controls.update();
    
    this.renderer.render(this.scene, this.camera);
  }

  private onWindowResize(): void {
    // Mantener la proporción al cambiar tamaño de ventana
    const width = window.innerWidth;
    const height = window.innerHeight;
    this.camera.aspect = width / height;
    this.camera.updateProjectionMatrix();
    this.renderer.setSize(width, height);
  }

  ngOnDestroy(): void {
    // Limpieza para evitar fugas de memoria
    if (this.animationId) cancelAnimationFrame(this.animationId);
  }

  public logCoordinates(): void {
  console.log('--- COPIA ESTOS NÚMEROS ---');
  
  // Posición de la cámara (dónde está parada)
  const pos = this.camera.position;
  console.log(`Cámara (Position): x: ${pos.x.toFixed(2)}, y: ${pos.y.toFixed(2)}, z: ${pos.z.toFixed(2)}`);
  
  // Punto de mira (hacia dónde está mirando)
  const target = this.controls.target;
  console.log(`Mira a (Target):   x: ${target.x.toFixed(2)}, y: ${target.y.toFixed(2)}, z: ${target.z.toFixed(2)}`);
  
  console.log('---------------------------');
  }
}